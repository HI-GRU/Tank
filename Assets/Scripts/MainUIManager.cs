using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    private static MainUIManager instance;
    public static MainUIManager Instance => instance;

    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;

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
        if (playButton != null)
        {
            playButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameScene");
            });
        }

        if (settingButton != null)
        {
            settingButton.onClick.AddListener(() =>
            {

            });
        }
    }
}
