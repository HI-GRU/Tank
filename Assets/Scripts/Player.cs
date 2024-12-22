using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [Header("Player Option")]
    private GameObject currentSkinObject;
    [SerializeField] private float rotationSpeed; // 회전 속도 (도/초)
    [SerializeField] private float moveSpeed;

    private float knockbackDuration;
    private bool isKnockedBack = false;
    private float knockbackTime;
    private Vector2 knockbackForce;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        LoadCurrentSkin();
    }

    private void Update()
    {
        if (isKnockedBack)
        {
            HandleKnockback();
            return;
        }

        // if (Input.touchCount > 0)
        // {
        //     Touch touch = Input.GetTouch(0);
        //     float mid = Screen.width / 2F;

        //     if (touch.position.x < mid)
        //     {
        //         transform.Rotate(0F, 0F, rotationSpeed * Time.deltaTime);
        //     }
        //     else
        //     {
        //         transform.Rotate(0F, 0F, -rotationSpeed * Time.deltaTime);
        //     }
        // }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0F, 0F, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0F, 0F, -rotationSpeed * Time.deltaTime);
        }

        Vector2 dir = transform.up;
        transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
    }

    private void LoadCurrentSkin()
    {
        GameObject skinPrefab = SkinManager.Instance.GetCurrentSkin();
        currentSkinObject = Instantiate(skinPrefab, transform);
        currentSkinObject.transform.localPosition = Vector3.zero;
    }

    public void ChangeSkin(GameObject newSkinPrefab)
    {
        if (currentSkinObject != null)
        {
            Destroy(currentSkinObject);
        }

        currentSkinObject = Instantiate(newSkinPrefab, transform);
        currentSkinObject.transform.localPosition = Vector3.zero;
    }

    private void HandleKnockback()
    {
        knockbackTime += Time.deltaTime;
        if (knockbackTime >= knockbackDuration)
        {
            isKnockedBack = false;
            knockbackTime = 0F;
            return;
        }

        float deceleration = 1F - (knockbackTime / knockbackDuration);
        Vector2 force = knockbackForce * deceleration;
        transform.position += (Vector3)force * Time.deltaTime;
    }

    public void ApplyKnockback(Vector2 force, float duration)
    {
        knockbackForce = force;
        knockbackDuration = duration;
        isKnockedBack = true;
        knockbackTime = 0F;
    }
}
