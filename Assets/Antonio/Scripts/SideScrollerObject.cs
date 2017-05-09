using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerObject : MonoBehaviour
{

    [SerializeField]
    float Speed;

    [SerializeField]
    float DestroyItSelfAfter = 5f;

    Rigidbody2D rb;

    float timer;

    private void OnEnable()
    {
        timer = 0;
    }

    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.position += -transform.right * Speed * Time.deltaTime;
            timer += Time.deltaTime;
            if (timer >= DestroyItSelfAfter)
            {
                timer = 0;
                gameObject.SetActive(false);
            }
        }
    }

}
