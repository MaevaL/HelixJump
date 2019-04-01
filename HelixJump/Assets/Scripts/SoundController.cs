using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    
    public AudioSource efxSource;
    public AudioSource musicSource;

    public AudioClip BounceClip;
    public AudioClip StepClip;

    public static SoundController instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    /// <summary>
    /// Play sound fx
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    /// <summary>
    /// Play music
    /// </summary>
    /// <param name="clip"></param>
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        efxSource.Play();
    }
}
