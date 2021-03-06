﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

	[SerializeField]
	GameObject countDownPanel;

    [SerializeField]
    Text countdownText; 

	[SerializeField]
	Sprite[] countDownNumbers;

    [SerializeField]
    KeyCode buttonToPress; 

	private bool inCountDown=false;
    private bool gameStarted = false;

    public GameObject eventSystem; 

	public static GameManager Instance;

	public int lives=3;


	void Awake(){
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
        countdownText.text = "PRESS " + buttonToPress + " TO START";  
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameStarted) {
			if (Input.GetKeyDown (buttonToPress)) {
				if (!inCountDown) {
					inCountDown = true;
					StartCoroutine (StartCountdown());
				}
			}
		}
	}

	IEnumerator StartCountdown(){
		Time.timeScale = 0;
        for (int i=0;i<countDownNumbers.Length;i++){
            countdownText.text = (countDownNumbers.Length - i).ToString(); 
			yield return new WaitForSecondsRealtime (1);
		}
        System.GC.Collect();
        countDownPanel.SetActive(false); 
		Time.timeScale = 1;
		inCountDown = false;
		gameStarted = true;
        AudioController.Instance.StartPlaying(); 
	}

	public void LoseLife(){
		lives--;
		if (lives <= 0) {
			GameOver ();
		} else {
			PlayerStatusManager.Instance.updateHUD.Invoke ();
		}
	}

	public void GameOver(){
        eventSystem.SetActive(true); 
		PlayerStatusManager.Instance.ShowGameOver ();
	}
}
