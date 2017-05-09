using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerObject : MonoBehaviour
{

    [SerializeField]
    float Speed;

    [SerializeField]
    float DestroyItSelfAfter = 5f;

    Vector3 movement = Vector3.zero; 

    float timer;

    private void OnEnable()
    {
        timer = 0;
    }

    void FixedUpdate()
    {
        if (gameObject.activeInHierarchy)
        {
            movement.x = -1 * Speed * Time.fixedDeltaTime;
            transform.position += movement; 
            timer += Time.fixedDeltaTime;
            if (timer >= DestroyItSelfAfter)
            {
                timer = 0;
                gameObject.SetActive(false);
            }
        }
    }

}
