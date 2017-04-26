using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float speed; 

	void LateUpdate () {
		speed=GameDifficultyUtility.Instance.getCameraSpeed();
        transform.position += new Vector3(speed, 0,0) * Time.deltaTime; 
	}
}
