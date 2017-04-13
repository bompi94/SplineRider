using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayColliders : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.CompareTag("Player")){
			Debug.Log ("Ball out of screen");
			GameManager.Instance.OnPlayerOutOfScreen.Invoke ();
		}
	}
}
