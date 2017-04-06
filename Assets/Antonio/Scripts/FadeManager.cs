using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FadeManager : MonoBehaviour {

	[SerializeField]
	float FadeOutSpeed=3f;

	[SerializeField]
	float FadeInSpeed=3f;

	CanvasGroup fadePanel;

	// Use this for initialization
	void Start () {
		fadePanel = GetComponent<CanvasGroup> ();
	}

	public void StartFadeOut(Action callback){
		StartCoroutine(FadeOut(callback));
	}

	IEnumerator FadeOut(Action callback){
		while (fadePanel.alpha < 0.99f) {
			fadePanel.alpha = Mathf.Lerp (fadePanel.alpha, 1f, Time.deltaTime * FadeOutSpeed);
			yield return new WaitForEndOfFrame ();
		}
		fadePanel.alpha = 1f;
        callback();
	}

	public void StartFadeIn(Action callback){
		StartCoroutine(FadeIn(callback));
	}

	IEnumerator FadeIn(Action callback){
		while (fadePanel.alpha > 0.01f) {
			fadePanel.alpha = Mathf.Lerp (fadePanel.alpha, 0f, Time.deltaTime * FadeInSpeed);
			yield return new WaitForEndOfFrame ();
		}
		fadePanel.alpha = 0f;
	}
}
