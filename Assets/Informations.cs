using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Informations : MonoBehaviour {

    public static Informations Instance;
    bool difficultMode; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject); 
    }

    public void SetDifficult(bool b)
    {
        difficultMode = b;
    }

    public bool IsDifficultMode()
    {
        return difficultMode; 
    }
}
