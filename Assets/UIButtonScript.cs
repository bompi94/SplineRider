using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonScript : MonoBehaviour {

	Animator animator;
	CanvasGroup canvas;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();
		canvas = GetComponent<CanvasGroup> ();
	}

	void OnEnable(){
		//Debug.Break ();
		canvas.alpha = 0;
		animator.SetTrigger ("OnEnable");
	}

	void OnDisable(){
		canvas.alpha = 0;
	}
}
