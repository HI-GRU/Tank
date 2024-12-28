using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance => instance;

    [Header("Player Option")]
    private GameObject currentSkinObject;
    [SerializeField] private float rotationSpeed; // 회전 속도 (도/초)
    [SerializeField] private float moveSpeed;

    private float knockbackDuration;
    private bool isKnockedBack = false;
    private float knockbackTime;
    private Vector2 knockbackForce;

    [SerializeField] private int rotateMode; // 1 : 조이스틱 , 2 : 좌우 화면 터치
    [SerializeField] private JoyStickHandler joyStick;

    private void Awake()
    {
        if (instance == null) instance = this;
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

        Rotate(rotateMode);
        Move();
    }

    private void LoadCurrentSkin()
    {
        if (currentSkinObject != null) Destroy(currentSkinObject);
        GameObject skinPrefab = SkinManager.Instance.GetCurrentSkin();
        currentSkinObject = Instantiate(skinPrefab, transform);
        currentSkinObject.transform.localPosition = Vector3.zero;
    }

    private void Rotate(int mode)
    {
        switch (mode)
        {
            case 1:
                JoyStickRotate();
                break;
            case 2:
                TouchRotate();
                break;
            default:
                break;
        }
    }

    private void JoyStickRotate()
    {
        Vector2 inputDir = joyStick.GetInputDir();
        if (inputDir == Vector2.zero) return;

        float currentAngle = transform.eulerAngles.z;
        float targetAngle = Mathf.Atan2(inputDir.y, inputDir.x) * Mathf.Rad2Deg - 90F;

        float angleDiff = Mathf.DeltaAngle(currentAngle, targetAngle);
        if (Mathf.Abs(angleDiff) < 1F) return;

        float rotationAmount = rotationSpeed * Time.deltaTime;

        if (angleDiff > 0) transform.Rotate(0F, 0F, rotationAmount);
        else if (angleDiff < 0) transform.Rotate(0F, 0F, -rotationAmount);
    }

    private void TouchRotate()
    {
        // if (Input.touchCount > 0)
        // {
        //     Touch touch = Input.GetTouch(0);
        //     float mid = Screen.width / 2F;
        //     float rotationAmount = rotationSpeed * Time.deltaTime;
        //     if (touch.position.x < mid) transform.Rotate(0F, 0F, rotationAmount);
        //     else if (touch.position.x > mid) transform.Rotate(0F, 0F, -rotationAmount);
        // }

        // Debug
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0F, 0F, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0F, 0F, -rotationSpeed * Time.deltaTime);
        }
    }

    private void Move()
    {
        Vector2 dir = transform.up;
        transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
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
