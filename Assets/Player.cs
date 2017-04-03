using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Vector2 force;
    Rigidbody2D rb;
    public float maximumVelocityMagnitude;

    private void Update()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>(); 
        if(rb.velocity.magnitude<maximumVelocityMagnitude)
            rb.AddForce(force); 
    }
}
