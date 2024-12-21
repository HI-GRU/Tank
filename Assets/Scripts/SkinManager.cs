using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance { get; private set; }

    [SerializeField] private List<GameObject> skinPrefabs;
    private int currentSkinIndex;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSelectedSkin();
    }

    private void LoadSelectedSkin()
    {
        currentSkinIndex = PlayerPrefs.GetInt("selectedSkinIndex", 0);
    }

    public GameObject GetCurrentSkin()
    {
        return skinPrefabs[currentSkinIndex];
    }

    public void SelectSkin(int index)
    {
        if (index >= 0 && index < skinPrefabs.Count)
        {
            currentSkinIndex = index;
            PlayerPrefs.SetInt("selectedSkinIndex", index);
            PlayerPrefs.Save();

            Player.Instance.ChangeSkin(GetCurrentSkin());
        }
    }
}
