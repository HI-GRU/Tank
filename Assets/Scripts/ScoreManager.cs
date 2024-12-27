using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance => instance;

    public int crownScore { get; private set; }

    [Header("Score Option")]
    [SerializeField] private int obstacleAttackScore;
    [SerializeField] private int obstacleCollisionScore;
    [SerializeField] private int weaponCollisionScore;
    [SerializeField] private TextMeshProUGUI crownScoreText;
    private float survivalTime;

    private void Awake()
    {
        if (instance == null) instance = this;
        crownScore = 0;
    }

    private void Update()
    {
        if (Player.Instance == null) return;
        survivalTime += Time.deltaTime;
    }

    private void AddScore(int points)
    {
        crownScore += points;
        if (crownScore < 0) crownScore = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (crownScoreText != null) crownScoreText.text = $"{crownScore:N0}";
    }

    public void UpdateObstacleAttackScore(int level)
    {
        AddScore(level * obstacleAttackScore);
    }

    public void UpdateObstacleCollisionScore()
    {
        AddScore(obstacleCollisionScore);
    }

    public void UpdateWeaponCollisionScore()
    {
        AddScore(weaponCollisionScore);
    }
}
