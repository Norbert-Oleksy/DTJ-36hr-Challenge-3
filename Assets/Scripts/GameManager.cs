using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.Utilities;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    #endregion
    #region Fields
    [Header("Game")]
    [SerializeField] Bomb bomb;
    [Space]
    [Header("INFO-UI")]
    [SerializeField] GameObject infoOBJ;
    [SerializeField] GameObject winINFO;
    [SerializeField] GameObject loseINFO;
    [Header("Phone")]
    [SerializeField] GameObject phoneObject;
    [Header("Sound")]
    [SerializeField] AudioSource explosionEmiter;
    [SerializeField] AudioClip audioClip;
    [SerializeField] AudioSource musicEmiter;
    #endregion
    #region Variables
    [HideInInspector] public gamestage stage;
    #endregion

    #region GameLogic
    public void GameOver()
    {
        infoOBJ.SetActive(true);
        loseINFO.SetActive(true);
        phoneObject.SetActive(false);
        stage = gamestage.Lose;
        explosionEmiter.Stop();
        musicEmiter.Stop();
        explosionEmiter.PlayOneShot(audioClip);
    }

    public void Victory()
    {
        explosionEmiter.Stop();
        infoOBJ.SetActive(true);
        winINFO.SetActive(true);
        phoneObject.SetActive(false);
        stage = gamestage.Win;
    }

    private IEnumerator StartAudioEffect()
    {
        float timer = 0f;

        while (timer < 1)
        {
            timer += Time.deltaTime;
            musicEmiter.volume = Mathf.Lerp(0f, 0.5f, timer / 1);
            yield return null;
        }
        musicEmiter.volume=0.5f;
        yield return null;
    }
    #endregion

    #region Unity-API
    private void Awake()
    {
        if (instance != null && instance != this) return;
        instance = this;
        stage = gamestage.None;
        if(explosionEmiter == null) explosionEmiter = GetComponent<AudioSource>();
        StartCoroutine(StartAudioEffect());
        bomb.SetABomb();
    }

    private void Update()
    {
        if(stage == gamestage.Pause && explosionEmiter.isPlaying)
        {
            explosionEmiter.Pause();
        }else if (stage == gamestage.Game && !explosionEmiter.isPlaying)
        {
            explosionEmiter.UnPause();
        }
    }

    #endregion

    #region enum
    public enum gamestage
    {
        None,
        Game,
        Pause,
        Win,
        Lose,
    }
    #endregion
}
