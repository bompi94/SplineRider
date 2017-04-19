using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceEffectorManager : MonoBehaviour {

	SurfaceEffector2D surface;
	// Use this for initialization
	void Start () {
		surface = GetComponent<SurfaceEffector2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		surface.speed = GameDifficultyUtility.Instance.getSurfaceEffectorForce ();
	}
}
