using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SetingsMenu : MonoBehaviour
{
    #region Fields
    [SerializeField] private Slider musicSlide;
    [SerializeField] private Slider soundSlide;

    [SerializeField] private TextMeshProUGUI musicValueDisplay;
    [SerializeField] private TextMeshProUGUI soundValueDisplay;
    #endregion

    #region Methods
    public void ChangeLanguage()
    {
        var currentLocale = LocalizationSettings.SelectedLocale;
        int nextIndex = LocalizationSettings.AvailableLocales.Locales.IndexOf(currentLocale) == 0? 1 : 0;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[nextIndex];
        DataManager.instance.languageId = nextIndex;
    }

    public void UpdateMusicValue(float value)
    {
        DataManager.instance.musicLevel = value;
        AudioManager.instance.SetVolume("MusicGroupParam", value);
        musicValueDisplay.text = (int)(value*100)+"%";
    }

    public void UpdateSoundValue(float value)
    {
        DataManager.instance.soundLevel = value;
        AudioManager.instance.SetVolume("SoundsGroupParam", value);
        soundValueDisplay.text = (int)(value * 100) + "%";
    }
    #endregion

    #region Unity-Api
    private void Awake()
    {
        musicSlide.value = DataManager.instance.musicLevel;
        soundSlide.value = DataManager.instance.soundLevel;
    }
    #endregion
}
