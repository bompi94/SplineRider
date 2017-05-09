using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float speed;
    Vector3 movement = Vector3.zero; 

	void FixedUpdate () {
		speed=GameDifficultyUtility.Instance.getCameraSpeed();
        movement.x = speed; 
        transform.position += movement * Time.fixedDeltaTime; 
	}
}
