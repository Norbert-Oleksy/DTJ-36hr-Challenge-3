using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject pauseOBJ;

    #endregion

    #region Functions

    private void PauseTheGame()
    {
        GameManager.instance.stage = GameManager.gamestage.Pause;
        pauseOBJ.SetActive(true);
    }

    public void ResumeTheGame()
    {
        GameManager.instance.stage = GameManager.gamestage.Game;
        pauseOBJ.SetActive(false);
    }

    public void GameRestert()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }


    #endregion

    #region Unity-API
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.stage == GameManager.gamestage.Game)
            {
                PauseTheGame();
            }else if (GameManager.instance.stage == GameManager.gamestage.Pause)
            {
                ResumeTheGame();
            }
        }
    }
    #endregion
}
