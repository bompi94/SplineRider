using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayColliders : MonoBehaviour {

    string playerTag = "Player"; 

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.CompareTag(playerTag)){
			GameManager.Instance.GameOver();
		}
	}
}
