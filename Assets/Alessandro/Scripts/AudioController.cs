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
        PlayWithNormalPitch(buttonClickClip); 
    }

    public void GotACoin()
    {
        PlayWithRandomPitch(0.95f, 1.05f, gotACoinClip); 
    }

    public void StartPlaying()
    {
        PlayWithNormalPitch(startPlayingClip);
    }

    public void GameOver()
    {
        PlayWithNormalPitch(gameOverClip);
    }

    public void LoseHealth()
    {
        PlayWithNormalPitch(damagedClip);
    }

    public void SetVolume(float volumeParam)
    {
        volume = volumeParam;
        aus.volume = volume; 
    }

    void PlayWithNormalPitch(AudioClip clip)
    {
        aus.pitch = 1;
        aus.PlayOneShot(clip);
    }

    void PlayWithRandomPitch(float minPitch, float maxPitch, AudioClip clip)
    {
        aus.pitch = Random.Range(minPitch, maxPitch);
        aus.PlayOneShot(clip);
    }
}
