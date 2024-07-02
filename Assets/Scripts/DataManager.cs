using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region Singleton
    public static DataManager instance;

    public void Initialization(Action endAction=null)
    {
        if(instance != null && instance != this) return;

        instance = this;
        DontDestroyOnLoad(this.gameObject);


        endAction?.Invoke();
    }

    #endregion

    #region DataRegion
    public int difficulty;
    public float soundLevel;
    public float musicLevel;
    #endregion
}