using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    private static SkinManager instance;
    public static SkinManager Instance => instance;

    [SerializeField] private List<GameObject> skinPrefabs;
    private int currentSkinIndex;
    private string skinPrefs = "selectedSkinIndex";

    private void Awake()
    {
        if (instance == null) instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSelectedSkin();
    }

    private void LoadSelectedSkin()
    {
        currentSkinIndex = PlayerPrefs.GetInt(skinPrefs, 0);
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
            PlayerPrefs.SetInt(skinPrefs, index);
            PlayerPrefs.Save();

            Player.Instance.ChangeSkin(GetCurrentSkin());
        }
    }
}
