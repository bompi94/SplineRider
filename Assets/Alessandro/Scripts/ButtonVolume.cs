using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVolume : MonoBehaviour {

    [SerializeField]
    GameObject otherButton;
    private void Awake()
    {
        if(AudioController.volume == 0)
        {
            otherButton.SetActive(true);
            gameObject.SetActive(false); 
        }
    }
}
