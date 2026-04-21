using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Música")]
    public AudioSource musicSource;
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    void Awake()
    {
        // singleton que persiste entre escenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        PlayMusic(menuMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null) return;
        if (musicSource.clip == clip) return; // evita reiniciar si ya suena

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void SetVolume(float volume)
    {
        if (musicSource != null)
            musicSource.volume = volume;
    }

    public void PlayGameMusic()
    {
        PlayMusic(gameMusic);
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }
}
