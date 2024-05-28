using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;


    [Header("Audio Clip")]
    public AudioClip _presentMusic;
    public AudioClip _pastMusic;
    public AudioClip _futurMusic;
    public AudioClip _deathCrush;
    public AudioClip _deathTruck;
    public AudioClip _fall;
    public AudioClip _forest;
    public AudioClip _futurSfxAmbiance;
    public AudioClip _laserShot;
    public AudioClip _riverSfx;
    public AudioClip _echoSfx;
    public AudioClip _splashSFX;
    public AudioClip _truck;
    public AudioClip _snowstorm;
    public AudioClip _jump;

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
}