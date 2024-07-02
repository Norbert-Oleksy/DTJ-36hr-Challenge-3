using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    #region Fields
    [SerializeField] private DataManager dataManager;
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
        dataManager.Initialization(()=> SetDataAsLoaded());
        yield return new WaitForSeconds(introTime);
        yield return new WaitUntil(()=>dataLoaded);
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