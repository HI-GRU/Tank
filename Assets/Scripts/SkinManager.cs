using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance { get; private set; }

    [SerializeField] private List<Sprite> skins;
    private int currentSkinIndex;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSkinSelection();
    }

    private void LoadSkinSelection()
    {
        currentSkinIndex = PlayerPrefs.GetInt("selectedSkinIndex", 0);
    }

    public Sprite GetCurrentSkin()
    {
        return skins[currentSkinIndex];
    }

    public void SelectSkin(int index)
    {
        if (index >= 0 && index < skins.Count)
        {
            currentSkinIndex = index;
            PlayerPrefs.SetInt("selectedSkinIndex", index);
            PlayerPrefs.Save();

            Player.Instance.ChangeSkin(GetCurrentSkin());
        }
    }
}
