using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetersSegnalator : MonoBehaviour {

	Transform segnalatorCreationPoint;
	Transform player;
	float playerLastXPosition;
	float meters;

	public GameObject metersSegnalatorPrefab;

	public int nextSegnalator=50;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerLastXPosition = player.position.x;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//calcolo la differenza tra la posizione vecchia e quella attuale
		float delta=player.position.x-playerLastXPosition;

		playerLastXPosition = player.position.x;
		meters += delta;
		if (meters >= nextSegnalator - transform.localPosition.x) {
			var segnalatorInstantiationX = player.position.x + transform.localPosition.x;
			var segnalator=Instantiate (metersSegnalatorPrefab, new Vector3(segnalatorInstantiationX,0,0), transform.rotation);
			segnalator.GetComponentInChildren<Text> ().text = nextSegnalator+"m";
			nextSegnalator += 50;
		}
	}
}
