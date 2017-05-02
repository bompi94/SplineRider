using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerObject : MonoBehaviour {

	[SerializeField]
	float Speed;

	[SerializeField]
	float DestroyItSelfAfter=5f;

    float timer;

    private void OnEnable()
    {
        timer = 0; 
    }

    // Update is called once per frame
    void Update () {
        if (gameObject.activeInHierarchy)
        {
            transform.Translate(-transform.right * Speed * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= DestroyItSelfAfter)
            {
                timer = 0;
                gameObject.SetActive(false);
            }
        }
	}

}
