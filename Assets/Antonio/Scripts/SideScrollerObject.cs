using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerObject : MonoBehaviour {

	[SerializeField]
	float Speed;

	[SerializeField]
	float DestroyItSelfAfter=5f;

	void Start(){
		Destroy (gameObject, DestroyItSelfAfter);
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (-transform.right * Speed * Time.deltaTime);
	}
}
