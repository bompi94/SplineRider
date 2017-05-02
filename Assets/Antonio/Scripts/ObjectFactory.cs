using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour {

	[SerializeField]
	float UpperY=0f;

	[SerializeField]
	float LowerY=0f;

	[SerializeField]
	float spawnTime=10f;

	[SerializeField]
	List<ObjectPooler> objectsPooler;

    GameObject scrollerObject; 

	private float counter;
	// Use this for initialization
	void Start () {
		counter = spawnTime;
	}
	
	// Update is called once per frame
	void Update () {
		counter -= Time.deltaTime;
		if (counter < 0) {
			Spawn ();
			counter = spawnTime;
		}
	}

	void Spawn(){
		float interval = UpperY + LowerY;
		float SpawnY = Random.Range (0, interval) - LowerY;

        int chosenScrollerIndex = Random.Range(0, objectsPooler.Count);

        scrollerObject = objectsPooler[chosenScrollerIndex].GetPooledObject();
		if (scrollerObject != null) {
			scrollerObject.transform.position = transform.position + new Vector3 (0, SpawnY, 0);
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position+new Vector3(0,UpperY,0), transform.position-new Vector3(0,LowerY,0));
	}


}
