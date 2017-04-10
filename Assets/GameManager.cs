using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	private bool gameStarted=false;

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
		for(int i=0;i<countDownNumbers.Length;i++){
            countdownText.text = (countDownNumbers.Length - i).ToString(); 
			yield return new WaitForSecondsRealtime (1);
		}
        countDownPanel.SetActive(false); 
		Time.timeScale = 1;
		inCountDown = false;
		gameStarted = true;
	}
}
