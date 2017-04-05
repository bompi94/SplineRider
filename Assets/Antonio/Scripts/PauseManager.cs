﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour {

	public static PauseManager Instance;

	public class BoolUnityEvent : UnityEvent<bool>{}

	public BoolUnityEvent OnPauseChanged;
	public BoolUnityEvent ChangePauseState;

	[SerializeField]
	float alphaInPause=0.5f;

	[SerializeField]
	float alphaInGame=0f;

	private bool pauseState=false;
	private CanvasGroup pausePanel;

	void Awake(){
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		pausePanel = GetComponent<CanvasGroup> ();
		OnPauseChanged = new BoolUnityEvent ();
		ChangePauseState = new BoolUnityEvent ();
		ChangePauseState.AddListener (SwitchPause);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			OnPauseChanged.Invoke (!pauseState);
			SwitchPause (!pauseState);
		}
	}

	void SwitchPause(bool inPause){
		if(inPause)
			GoToPause();
		else
			GoToGame();
	}

	void GoToPause(){
		pauseState = true;
		Time.timeScale = 0;
		pausePanel.alpha = alphaInPause;
	}

	public void GoToGame(){
		pauseState = false;
		Time.timeScale = 1f;
		pausePanel.alpha = alphaInGame;
	}
}