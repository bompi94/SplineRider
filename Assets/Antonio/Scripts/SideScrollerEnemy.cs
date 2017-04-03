using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerEnemy : SideScrollerObject {

	void OnTriggerEnter(Collider coll){
		if (coll.CompareTag ("Player")) {
			PlayerStatusManager.Instance.ReceiveHit.Invoke ();
		}
	}
}
