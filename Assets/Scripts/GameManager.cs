using System.Collections;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    [HideInInspector] public Camera mainCamera;

    [Header("UI Panels")]
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI crownScoreText;
    [SerializeField] private TextMeshProUGUI survivalScoreText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private float gameOverLoadingTime;

    private void Awake()
    {
        if (instance == null) instance = this;
        mainCamera = Camera.main;
        SetCurrentState(GameState.Playing);
    }

    public bool IsPointInCamera(Vector2 point)
    {
        Vector2 viewport = mainCamera.WorldToViewportPoint(point);
        return viewport.x >= 0 && viewport.x <= 1 && viewport.y >= 0 && viewport.y <= 1;
    }

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    private GameState currentState;

    public void SetCurrentState(GameState state)
    {
        currentState = state;
        switch (currentState)
        {
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.Paused:
                HandlePaused();
                break;
            case GameState.GameOver:
                StartCoroutine(HandleGameOver());
                break;
            default:
                break;
        }
    }

    private void HandlePlaying()
    {
        Play();
        playPanel.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void HandlePaused()
    {
        Stop();
        playPanel.SetActive(true);
        pausePanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    private IEnumerator HandleGameOver()
    {
        CalculateScore();

        yield return new WaitForSeconds(gameOverLoadingTime);
        Stop();
        playPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    private void Play()
    {
        Time.timeScale = 1F;
    }

    private void Stop()
    {
        Time.timeScale = 0F;
    }

    private void CalculateScore()
    {
        int crownScore = ScoreManager.Instance.crownScore;
        int survivalScore = (int)ScoreManager.Instance.survivalTime;
        int totalScore = crownScore + survivalScore;

        UpdateScoreUI(crownScore, survivalScore, totalScore);
    }

    private void UpdateScoreUI(int crownScore, int survivalScore, int totalScore)
    {
        if (crownScoreText != null) crownScoreText.text = $"+ {crownScore:N0}";
        if (survivalScoreText != null) survivalScoreText.text = $"+ {survivalScore:N0}";
        if (totalScoreText != null) totalScoreText.text = $"+ {totalScore:N0}";
    }
}
