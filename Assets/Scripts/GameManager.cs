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
}
