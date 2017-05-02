using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerObject : MonoBehaviour {

	[SerializeField]
	float Speed;

	[SerializeField]
	float DestroyItSelfAfter=5f;

    Rigidbody2D rb; 

    float timer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void OnEnable()
    {
        timer = 0; 
    }

    void Update () {
        if (gameObject.activeInHierarchy)
        {
            rb.MovePosition(transform.position += -transform.right * Speed * Time.deltaTime); 
            timer += Time.deltaTime;
            if (timer >= DestroyItSelfAfter)
            {
                timer = 0;
                gameObject.SetActive(false);
            }
        }
	}

}
