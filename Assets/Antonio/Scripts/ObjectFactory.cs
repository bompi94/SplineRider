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
		var interval = UpperY + LowerY;
		var SpawnY = Random.Range (0, interval) - LowerY;

		var prefabToInstantiate = Random.Range (0, objectsPooler.Count);

		var positionToInstantiate = transform.position + new Vector3 (0, SpawnY, 0);

        GameObject obj = objectsPooler[prefabToInstantiate].GetPooledObject();
        obj.transform.position = positionToInstantiate; 
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position+new Vector3(0,UpperY,0), transform.position-new Vector3(0,LowerY,0));
	}


}
