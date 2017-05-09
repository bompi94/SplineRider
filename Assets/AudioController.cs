using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    [HideInInspector]
    public AudioSource aus;

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

    }

    public void LoseHealth()
    {

    }
}
