using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float speed;
    Vector3 movement = Vector3.zero;
    GameDifficultyUtility diff;

    private void Start()
    {
        diff = GameDifficultyUtility.Instance; 
    }

    void FixedUpdate () {
		speed=diff.getCameraSpeed();
        movement.x = speed * Time.fixedDeltaTime; 
        transform.position += movement; 
	}
}
