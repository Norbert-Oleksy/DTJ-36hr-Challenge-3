using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class PhoneMenu : MonoBehaviour
{
    [Header("MenuSections")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject dificultyMenu;
    [SerializeField] private GameObject setingsMenu;
    [SerializeField] private GameObject infoMenu;
    [Header("Other")]
    [SerializeField] private Animator animator;
    private bool animationIsOn = false;

    public void OpenCloseDificultyMenu(bool value)
    {
        mainMenu.SetActive(!value);
        dificultyMenu.SetActive(value);
    }

    public void OpenCloseSettingsMenu(bool value)
    {
        //mainMenu.SetActive(!value);
        //setingsMenu.SetActive(value);
        animationIsOn = true;

        if (value)
        {
            animator.Play("FlipAPhone");
            mainMenu.SetActive(false);
        }
        else
        {
            animator.Play("UnFlipAPhone");
            setingsMenu.SetActive(false);
            DataManager.instance.SaveData();
        }
        
        StartCoroutine(DoAfterDelay(() => {
            mainMenu.SetActive(!value);
            setingsMenu.SetActive(value);
            animationIsOn = false;
        }, 0.2f));
    }
    public void OpenCloseInfoMenu(bool value)
    {
        //mainMenu.SetActive(!value);
        //infoMenu.SetActive(value);
        animationIsOn = true;

        if (value)
        {
            animator.Play("FlipAPhone");
            mainMenu.SetActive(false);
        }
        else
        {
            animator.Play("UnFlipAPhone");
            infoMenu.SetActive(false);
        }

        StartCoroutine(DoAfterDelay(() =>{
            mainMenu.SetActive(!value);
            infoMenu.SetActive(value);
            animationIsOn = false;
        },0.2f));
    }

    private IEnumerator DoAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
        yield return null;
    }

    public void PlayLevel(int dificulty)
    {
        DataManager.instance.difficulty = dificulty;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    private void Update()
    {
        if(animationIsOn) return;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (dificultyMenu.activeSelf)
            {
                OpenCloseDificultyMenu(false);
            }
            else if (setingsMenu.activeSelf)
            {
                OpenCloseSettingsMenu(false);
            }
            else if (infoMenu.activeSelf)
            {
                OpenCloseInfoMenu(false);
            }
        }
    }

    private void Awake()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[DataManager.instance.languageId];
    }
}
