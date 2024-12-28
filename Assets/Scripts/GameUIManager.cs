using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    private static GameUIManager instance;
    public static GameUIManager Instance => instance;

    [Header("UI Buttons")]
    [SerializeField] private Button homeButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button retryButton;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        if (homeButton != null)
        {
            homeButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("MainScene");
            });
        }

        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(() =>
            {
                GameManager.Instance.SetCurrentState(GameManager.GameState.Paused);
            });
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(() =>
            {
                GameManager.Instance.SetCurrentState(GameManager.GameState.Playing);
            });
        }

        if (retryButton != null)
        {
            retryButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameScenes");
            });
        }
    }
}
