using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region Singleton
    public static DataManager instance;
    #endregion
    #region Methods
    public void Initialization(Action endAction=null)
    {
        musicLevel = PlayerPrefs.GetFloat("musicLevel", 0.8f);
        soundLevel = PlayerPrefs.GetFloat("soundLevel", 0.8f);
        languageId = PlayerPrefs.GetInt("chosenLanguage", 0);
        endAction?.Invoke();
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("musicLevel", musicLevel);
        PlayerPrefs.SetFloat("soundLevel", soundLevel);
        PlayerPrefs.SetInt("chosenLanguage", languageId);
        PlayerPrefs.Save();
    }
    #endregion

    #region DataRegion
    public int difficulty; // 1 - easy, 2 - normal, 3 - hard
    public float soundLevel;
    public float musicLevel;
    public int languageId;
    #endregion

    #region Unity-API
    private void Awake()
    {
        if (instance != null && instance != this) return;
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
}