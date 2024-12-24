using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Camera mainCamera;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        mainCamera = Camera.main;
    }

    public bool IsPointInCamera(Vector2 point)
    {
        Vector2 viewport = mainCamera.WorldToViewportPoint(point);
        return viewport.x >= 0 && viewport.x <= 1 && viewport.y >= 0 && viewport.y <= 1;
    }
}
