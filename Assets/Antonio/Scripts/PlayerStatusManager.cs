using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerStatusManager : MonoBehaviour {

	public class FloatUnityEvent:UnityEvent<float>{}

	public static PlayerStatusManager Instance;

	private Image[] heartImages;
	private Text scoreText;

	private int lives=3;
	private float score=0;

	public UnityEvent ReceiveHit;
	public FloatUnityEvent AddPoint;

	void Awake(){
		if (Instance == null) {
			Instance = this;
		} else
			Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		ReceiveHit = new UnityEvent ();
		ReceiveHit.AddListener (LoseLives);
		AddPoint = new FloatUnityEvent ();
		AddPoint.AddListener (AddPoints);
		scoreText=GetComponentInChildren<Text> ();
		heartImages=transform.FindChild ("Lives").GetComponentsInChildren<Image> ();
		UpdateHUD ();
	}

	void AddPoints(float amount){
		score += amount;
	}
	
	void LoseLives(){
		lives--;
		UpdateHUD ();
	}

	void UpdateHUD(){
		scoreText.text = "Score:" + score;
		for (int i = 0; i < heartImages.Length; i++) {
			if (i + 1 <= lives) {
				heartImages [i].color = Color.white;
			} else {
				heartImages [i].color = new Color (1, 1, 1, 0);
			}
		}
	}
}
