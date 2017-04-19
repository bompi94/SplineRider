using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
    public float speed; 

	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z); 
		speed=GameDifficultyUtility.Instance.getCameraSpeed();
        transform.position += new Vector3(speed, 0,0) * Time.deltaTime; 
	}
}
