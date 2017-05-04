using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegnalatorObject : MonoBehaviour {

	[SerializeField]
	float DestroyItSelfAfter=8f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, DestroyItSelfAfter);
	}
}
