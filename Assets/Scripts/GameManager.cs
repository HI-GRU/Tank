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
                HandleGameOver();
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

    private void HandleGameOver()
    {
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
}
