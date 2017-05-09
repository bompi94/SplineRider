using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetersSegnalator : MonoBehaviour
{
    Transform segnalatorCreationPoint;
    Transform player;
    float playerLastXPosition;
    float meters;

    GameObject segnalator;
    Text segnalatorText; 
    public GameObject metersSegnalatorPrefab;

    public int nextSegnalator = 50;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerLastXPosition = player.position.x;
        segnalator = Instantiate(metersSegnalatorPrefab); 
        segnalatorText = segnalator.GetComponentInChildren<Text>();
        segnalator.transform.position = new Vector3(player.transform.position.x + nextSegnalator, 0, 0);
        segnalatorText.text = nextSegnalator + "m";
        nextSegnalator += 50;
    }

    void FixedUpdate()
    {
        //calcolo la differenza tra la posizione vecchia e quella attuale
        float delta = player.position.x - playerLastXPosition;

        playerLastXPosition = player.position.x;
        meters += delta;
        if (meters >= nextSegnalator - transform.localPosition.x)
        {
            UpdateSegnalator(); 
        }
    }

    void UpdateSegnalator()
    {
        var segnalatorInstantiationX = player.position.x + transform.localPosition.x;
        segnalator.transform.position = new Vector3(segnalatorInstantiationX, 0, 0); 
        segnalatorText.text = nextSegnalator + "m";
        nextSegnalator += 50;
    }
}
