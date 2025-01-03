using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEmiter : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    #region Methods

    public void PlayClipById(int id=0)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClips[id]);
    }

    public void PlayRandomClip()
    {
        PlayClipById(Random.Range(0,audioClips.Length));
    }

    #endregion

    #region Unity-API
    private void Awake()
    {
        if(audioSource != null) return;

        audioSource = GetComponent<AudioSource>();
    }
    #endregion
}
