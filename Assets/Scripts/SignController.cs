using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SingController : MonoBehaviour
{
    [Header("Warning Option")]
    [SerializeField] private GameObject signPrefab;
    [SerializeField] private float offset;

    private GameObject sign;
    private bool isActive = false;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
        sign = Instantiate(signPrefab);
        sign.SetActive(isActive);
    }

    private void Update()
    {
        UpdateSign();
    }

    private void UpdateSign()
    {
        if (!enabled || Player.Instance == null) return;

        bool setActive = !GameManager.Instance.IsPointInCamera(transform.position);
        if (isActive != setActive)
        {
            sign.SetActive(setActive);
            isActive = setActive;
        }

        if (!isActive) return;
        sign.transform.position = UpdateSignPosition();
    }

    private Vector2 UpdateSignPosition()
    {
        // 플레이어와 현재 오브젝트의 방향 벡터를 계산
        Vector2 dir = (Vector2)(transform.position - Player.Instance.transform.position).normalized;

        // 현재 오브젝트의 월드 좌표
        Vector2 worldPos = transform.position;

        // 카메라의 뷰포트 경계를 월드 좌표로 변환
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // 월드 좌표에서의 경계선 정의 (offset 적용)
        float left = bottomLeft.x + offset;
        float right = topRight.x - offset;
        float bottom = bottomLeft.y + offset;
        float top = topRight.y - offset;

        // 교점 초기화
        Vector2 intersection = worldPos;

        // x 방향으로 교점 계산
        if (dir.x != 0)
        {
            float t = dir.x > 0 ? (right - worldPos.x) / dir.x : (left - worldPos.x) / dir.x;
            Vector2 candidate = worldPos + t * dir;
            if (candidate.y >= bottom && candidate.y <= top)
                intersection = candidate;
        }

        // y 방향으로 교점 계산
        if (dir.y != 0)
        {
            float t = dir.y > 0 ? (top - worldPos.y) / dir.y : (bottom - worldPos.y) / dir.y;
            Vector2 candidate = worldPos + t * dir;
            if (candidate.x >= left && candidate.x <= right)
                intersection = candidate;
        }

        return intersection;
    }



    private void OnDestroy()
    {
        if (sign != null) Destroy(sign);
    }
}
