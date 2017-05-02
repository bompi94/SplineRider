using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerCoin : SideScrollerObject {

	[SerializeField]
	float points=100;

    string playerTag = "Player"; 


	void OnTriggerEnter2D(Collider2D coll){
		if (coll.CompareTag (playerTag)) {
			PlayerStatusManager.Instance.AddPoint.Invoke (points);
		}
        gameObject.SetActive(false);  
	}
}
