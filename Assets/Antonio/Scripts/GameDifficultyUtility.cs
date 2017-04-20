using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameDifficultyUtility : MonoBehaviour
{

    public static GameDifficultyUtility Instance;

    private float surfaceEffectorSpeedBasic = 4;
    private float cameraSpeedBasic = 3;

    public bool difficultMode;

    [SerializeField]
    private float DifficultyUpdateInterval = 1f;

    [SerializeField]
    private float SurfaceEffectorMinSpeed = 3f;

    [SerializeField]
    private float SurfaceEffectorMaxSpeed = 300f;

    [SerializeField]
    private float SurfaceEffectorIncrementPercentage = 5f;

    [SerializeField]
    private float CameraMinSpeed = 4f;

    [SerializeField]
    private float CameraMaxSpeed = 300f;

    [SerializeField]
    private float CameraSpeedIncrementPercentage = 5f;

    [SerializeField]
    private float currentCameraSpeed;
    [SerializeField]
    private float currentSurfaceEffectorSpeed;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (Informations.Instance != null) // Informations comes from main menu, if it is absent the variable on game manager will be the one chosen for difficulty
                difficultMode = Informations.Instance.IsDifficultMode();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        currentCameraSpeed = CameraMinSpeed;
        currentSurfaceEffectorSpeed = SurfaceEffectorMinSpeed;
        PlayerStatusManager.Instance.SetDifficult(difficultMode); 
        if (difficultMode)
            AdjustDifficulty();
    }

    public void AdjustDifficulty()
    {
        //calcolo il 5% del valore attuale, quindi incremento il valore attuale di quella quantità
        float tmp = (currentCameraSpeed * CameraSpeedIncrementPercentage) / 100;
        currentCameraSpeed += tmp;

        //calcolo il 5% del valore attuale, quindi incremento il valore attuale di quella quantità
        tmp = (currentSurfaceEffectorSpeed * SurfaceEffectorIncrementPercentage) / 100;
        currentSurfaceEffectorSpeed += tmp;

        Invoke("AdjustDifficulty", DifficultyUpdateInterval);
    }

    public float getCameraSpeed()
    {
        if (difficultMode)
            return Mathf.Clamp(currentCameraSpeed, CameraMinSpeed, CameraMaxSpeed);
        return cameraSpeedBasic;
    }

    public float getSurfaceEffectorForce()
    {
        if (difficultMode)
            return Mathf.Clamp(currentSurfaceEffectorSpeed, SurfaceEffectorMinSpeed, SurfaceEffectorMaxSpeed);
        return surfaceEffectorSpeedBasic;
    }
}
