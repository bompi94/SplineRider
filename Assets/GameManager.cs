using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField]
	Image countDownImage;

	[SerializeField]
	Sprite[] countDownNumbers;

	private bool inCountDown=false;
	private bool gameStarted=false;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		countDownImage.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStarted) {
			if (Input.GetKeyDown (KeyCode.Return)) {
				if (!inCountDown) {
					inCountDown = true;
					StartCoroutine (StartCountdown());
				}
			}
		}
	}

	IEnumerator StartCountdown(){
		countDownImage.enabled = true;
		Time.timeScale = 0;
		for(int i=0;i<countDownNumbers.Length;i++){
			countDownImage.sprite = countDownNumbers [i];
			yield return new WaitForSecondsRealtime (1);
		}
		countDownImage.enabled = false;
		Time.timeScale = 1;
		inCountDown = false;
		gameStarted = true;
	}
}
