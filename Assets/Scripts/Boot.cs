using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    #region Fields
    [SerializeField] private DataManager dataManager;
    [SerializeField] private AudioManager audioManager;
    //[SerializeField] private animatior
    [SerializeField] private float introTime;
    #endregion

    private bool dataLoaded = false;

    #region LogicRegion
    private void Awake()
    {
        StartCoroutine(BootProces());
    }

    private IEnumerator BootProces()
    {
        dataManager.Initialization(()=> {
            audioManager.Initialization(() => SetDataAsLoaded());
        });
        //yield return new WaitForSeconds(introTime);
        yield return new WaitUntil(()=>dataLoaded);
        yield return new WaitUntil(() => LocalizationSettings.InitializationOperation.IsDone);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[DataManager.instance.languageId];
        LoadMainMenu();
        yield return null;
    }

    private void SetDataAsLoaded()
    {
        dataLoaded = true;
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }
    #endregion
}