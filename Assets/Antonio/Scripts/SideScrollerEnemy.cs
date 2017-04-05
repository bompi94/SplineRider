using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerEnemy : SideScrollerObject {

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.CompareTag ("Player")) {
			PlayerStatusManager.Instance.ReceiveHit.Invoke ();
            Destroy(gameObject); 
		}
	}
}
