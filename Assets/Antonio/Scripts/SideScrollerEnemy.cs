﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerEnemy : SideScrollerObject {
    string playerTag = "Player";
    string hit = "Hit"; 
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.CompareTag (playerTag)) {
            GameManager.Instance.LoseLife();
            gameObject.SetActive(false);
            AudioController.Instance.LoseHealth();
            coll.GetComponent<Animator> ().SetTrigger (hit);
		}
	}
}
