using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using System;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager instance;
    #endregion

    #region Fields
    [SerializeField] private AudioMixer audioMixer;
    #endregion

    #region Methods
    public void SetVolume(string group,float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1.0f);
        audioMixer.SetFloat(group, Mathf.Log10(volume) * 20);
    }

    public void Initialization(Action endAction = null)
    {
        if (instance != null && instance != this) return;

        DontDestroyOnLoad(this.gameObject);

        instance = this;

        SetVolume("MusicGroupParam", DataManager.instance.musicLevel);
        SetVolume("SoundsGroupParam", DataManager.instance.soundLevel);

        endAction?.Invoke();
    }
    #endregion
}