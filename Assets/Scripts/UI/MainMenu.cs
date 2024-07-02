using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]  private GameObject mainMenu;
    [SerializeField]  private GameObject dificultyMenu;

    public void OpenCloseDificultyMenu(bool value)
    {
        mainMenu.SetActive(!value);
        dificultyMenu.SetActive(value);
    }

    public void PlayLevel(int dificulty)
    {
        DataManager.instance.difficulty = dificulty;
        SceneManager.LoadScene("Game",LoadSceneMode.Single);

    }
}
