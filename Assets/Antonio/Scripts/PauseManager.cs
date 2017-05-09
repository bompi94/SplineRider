using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

	public static PauseManager Instance;

	public class BoolUnityEvent : UnityEvent<bool>{}

	public BoolUnityEvent OnPauseChanged;
	public BoolUnityEvent ChangePauseState;

    EventSystem es; 

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
        es = GameObject.FindObjectOfType<EventSystem>();
        es.gameObject.SetActive(false); 
		pausePanel = GetComponent<CanvasGroup> ();
		OnPauseChanged = new BoolUnityEvent ();
		ChangePauseState = new BoolUnityEvent ();
		ChangePauseState.AddListener (SwitchPause);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && (Time.timeScale!=0 || pauseState)) {
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
        es.gameObject.SetActive(true); 
		pauseState = true;
		Time.timeScale = 0;
		pausePanel.alpha = alphaInPause;
        pausePanel.interactable = true; 
		pausePanel.blocksRaycasts = true;
        System.GC.Collect();
    }

	public void GoToGame(){
        es.gameObject.SetActive(false);
        pauseState = false;
		Time.timeScale = 1f;
		pausePanel.alpha = alphaInGame;
        pausePanel.interactable = false;
		pausePanel.blocksRaycasts = false;
	}

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }
}
