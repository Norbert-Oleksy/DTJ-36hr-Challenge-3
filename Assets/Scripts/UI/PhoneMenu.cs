using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneMenu : MonoBehaviour
{
    [Header("MenuSections")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject dificultyMenu;
    [SerializeField] private GameObject setingsMenu;
    [Header("Other")]
    [SerializeField] private Animator animator;

    public void OpenCloseDificultyMenu(bool value)
    {
        mainMenu.SetActive(!value);
        dificultyMenu.SetActive(value);
    }
    public void OpenCloseSettingsMenu(bool value)
    {
        mainMenu.SetActive(!value);
        setingsMenu.SetActive(value);
    }

    public void PlayLevel(int dificulty)
    {
        DataManager.instance.difficulty = dificulty;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
