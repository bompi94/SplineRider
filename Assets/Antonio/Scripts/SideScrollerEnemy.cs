using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerEnemy : SideScrollerObject {
    string playerTag = "Player";
    string hit = "Hit"; 
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.CompareTag (playerTag)) {
			GameManager.Instance.OnPlayerLosesLife.Invoke ();
            gameObject.SetActive(false); 
			coll.GetComponent<Animator> ().SetTrigger (hit);
		}
	}
}
