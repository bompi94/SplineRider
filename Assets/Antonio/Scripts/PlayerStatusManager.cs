using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerStatusManager : MonoBehaviour
{

    public class FloatUnityEvent : UnityEvent<float> { }

    public static PlayerStatusManager Instance;

    private Image[] heartImages;
    private Text scoreText;
    public Text bestScoreText;
    public Text kindOfBestScoreText; 

    private float score = 0;
    private float bestScore = 0;

    private string bestScoreSaveKeyRelax = "bestRelax";
    private string bestScoreSaveKeyChallenge = "bestChallenge";
    string scoreString = "Score: "; 
    string bestScoreSaveKey;

    private string bestScoreString; 

    public UnityEvent updateHUD;
    public FloatUnityEvent AddPoint;


    private CanvasGroup GameOverPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    void AddPoints(float amount)
    {
        score += amount;
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetFloat(bestScoreSaveKey, bestScore);
        }
        UpdateHUD();
    }

    void UpdateHUD()
    {
        scoreText.text =score.ToString ();
        bestScoreText.text = bestScore.ToString();
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i + 1 <= GameManager.Instance.lives)
            {
                heartImages[i].color = Color.white;
            }
            else
            {
                heartImages[i].color = new Color(1, 1, 1, 0);
            }
        }
    }


    public void ShowGameOver()
    {
        GameOverPanel.alpha = 1;
        GameOverPanel.interactable = true;
        Time.timeScale = 0;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetDifficult(bool b)
    {
        if (b)
        {
            bestScoreSaveKey = bestScoreSaveKeyChallenge;
            kindOfBestScoreText.text = "Best (challenge): "; 
        }

        else
        {
            bestScoreSaveKey = bestScoreSaveKeyRelax;
            kindOfBestScoreText.text = "Best (relax): ";
        }

        if (PlayerPrefs.HasKey(bestScoreSaveKey))
        {
            bestScore = PlayerPrefs.GetFloat(bestScoreSaveKey);
        }
        else
        {
            PlayerPrefs.SetFloat(bestScoreSaveKey, 0);
        }
        GameOverPanel = transform.FindChild("GameOver").GetComponent<CanvasGroup>();
        GameOverPanel.alpha = 0;
        GameOverPanel.interactable = false;
        updateHUD = new UnityEvent();
        updateHUD.AddListener(UpdateHUD);
        AddPoint = new FloatUnityEvent();
        AddPoint.AddListener(AddPoints);
        scoreText = GetComponentInChildren<Text>();
        heartImages = transform.FindChild("Lives").GetComponentsInChildren<Image>();

        UpdateHUD();
    }
}
