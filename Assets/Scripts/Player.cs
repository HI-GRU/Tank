using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private SpriteRenderer skin;
    [SerializeField] private float rotationSpeed; // 회전 속도 (도/초)
    [SerializeField] private float moveSpeed;

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
        Sprite selectedSkin = SkinManager.Instance.GetCurrentSkin();
        skin.sprite = selectedSkin;
    }

    public void ChangeSkin(Sprite newSkin)
    {
        skin.sprite = newSkin;
    }
}
