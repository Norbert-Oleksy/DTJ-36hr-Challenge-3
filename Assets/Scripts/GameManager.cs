using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion
    #region Variables
    [HideInInspector] public gamestage stage;
    #endregion

    #region GameLogic
    public void GameOver()
    {
        infoOBJ.SetActive(true);
        loseINFO.SetActive(true);
        stage = gamestage.Lose;
    }

    public void Victory()
    {
        infoOBJ.SetActive(true);
        winINFO.SetActive(true);
        stage = gamestage.Win;
    }
    #endregion

    #region Unity-API
    private void Awake()
    {
        if (instance != null && instance != this) return;
        instance = this;
        stage = gamestage.None;
        bomb.SetABomb();
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
