using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    [HideInInspector]
    public AudioSource aus;

    public static float volume = 1;
    public static AudioController Instance; 

    public AudioClip buttonClickClip;
    public AudioClip gotACoinClip;
    public AudioClip startPlayingClip; 
    public AudioClip gameOverClip;
    public AudioClip damagedClip;

    private void Awake()
    {
        Instance = this;
        aus = GetComponent<AudioSource>();
        aus.volume = volume; 
    }

    public void ButtonClick()
    {
        aus.PlayOneShot(buttonClickClip); 
    }

    public void GotACoin()
    {
        aus.PlayOneShot(gotACoinClip);
    }

    public void StartPlaying()
    {
        aus.PlayOneShot(startPlayingClip);
    }

    public void GameOver()
    {
        aus.PlayOneShot(gameOverClip);
    }

    public void LoseHealth()
    {
        aus.PlayOneShot(damagedClip);
    }

    public void SetVolume(float volumeParam)
    {
        volume = volumeParam;
        aus.volume = volume; 
    }
}
