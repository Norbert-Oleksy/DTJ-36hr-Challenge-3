using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class PauseMenu : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject pauseOBJ,phoneImage,optionsMenu,blockRaycasts;
    [SerializeField] private Animator animator;
    #endregion

    #region Events
    [SerializeField] private UnityEvent onPause;
    [SerializeField] private UnityEvent onResume;
    #endregion

    #region Variables
    private bool isPhoneAnimaiting=false;
    #endregion
    #region Functions

    private void PauseTheGame()
    {
        GameManager.instance.stage = GameManager.gamestage.Pause;
        pauseOBJ.SetActive(true);
        phoneImage.SetActive(true);
        blockRaycasts.SetActive(true);
        onPause?.Invoke();
    }

    public void ResumeTheGame()
    {
        GameManager.instance.stage = GameManager.gamestage.Game;
        pauseOBJ.SetActive(false);
        phoneImage.SetActive(false);
        blockRaycasts.SetActive(false);
        onResume?.Invoke();
    }

    public void GameRestert()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OpenCloseOptions(bool value)
    {
        isPhoneAnimaiting = true;

        if (value)
        {
            animator.Play("FlipAPhone");
            pauseOBJ.SetActive(false);
        }
        else
        {
            animator.Play("UnFlipAPhone");
            optionsMenu.SetActive(false);
        }

        StartCoroutine(DoAfterDelay(() => {
            pauseOBJ.SetActive(!value);
            optionsMenu.SetActive(value);
            isPhoneAnimaiting = false;
        }, 0.2f));
    }

    private IEnumerator DoAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
        yield return null;
    }

    #endregion

    #region Unity-API
    private void Update()
    {
        if (!isPhoneAnimaiting && Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.stage == GameManager.gamestage.Game)
            {
                PauseTheGame();
            }else if (GameManager.instance.stage == GameManager.gamestage.Pause)
            {
                if (optionsMenu.activeSelf)
                {
                    OpenCloseOptions(false);
                }
                else
                {
                    ResumeTheGame();
                }
            }
        }
    }
    #endregion
}
