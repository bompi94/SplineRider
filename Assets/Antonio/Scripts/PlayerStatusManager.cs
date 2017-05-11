using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Text;

public class PlayerStatusManager : MonoBehaviour
{

    public class IntUnityEvent : UnityEvent<int> { }

    public static PlayerStatusManager Instance;

    private Image[] heartImages;
    private Text scoreText;
    public Text bestScoreText;
    public Text kindOfBestScoreText; 

    private int score = 0;
    private int bestScore = 0;

    private string bestScoreSaveKeyRelax = "bestRelax";
    private string bestScoreSaveKeyChallenge = "bestChallenge";
    string bestScoreSaveKey;

    private string bestScoreString; 

    public UnityEvent updateHUD;
    public IntUnityEvent AddPoint;

    private CanvasGroup GameOverPanel;

	private Transform player;
	private float lastPlayerXValue;

	private float meters = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

	void Start(){
		player = GameObject.FindWithTag ("Player").transform;
		lastPlayerXValue = player.position.x;
	}

	void FixedUpdate(){
		//calcolo la differenza tra la posizione vecchia e quella attuale
		float delta=player.position.x-lastPlayerXValue;

		//lo spostamento potrebbe anche essere verso sinistra, in quel caso non faccio nulla
		if (delta > 0) {
			lastPlayerXValue = player.position.x;
			meters += delta;
		}
	}

    void AddPoints(int amount)
    {
        score += amount;
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
        AudioController.Instance.GameOver(); 
        GameOverPanel.alpha = 1;
        GameOverPanel.interactable = true;
        Time.timeScale = 0;
		var resultsPanel = GameOverPanel.transform.Find ("Results");

		var scoreText = resultsPanel.Find ("ScoreTitle").GetComponent<Text> ();
		var scoreValue = resultsPanel.Find ("Score").GetComponent<Text> ();
		var newScoreText = resultsPanel.Find ("NewScoreTitle").GetComponent<Text> ();
		var metersText = resultsPanel.Find ("Meters").GetComponent<Text> ();

		scoreValue.text = score.ToString();
		metersText.text = meters.ToString("0.#");
		if (score > bestScore) {
			StartCoroutine (NewBestScoreAnimation (scoreText, newScoreText));
			PlayerPrefs.SetInt(bestScoreSaveKey, score);
		}

		System.GC.Collect(); 
    }


    public void RestartGame()
    {
		StopCoroutine ("NewBestScoreAnimation");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public void ToMainMenu()
	{
		StopCoroutine ("NewBestScoreAnimation");
        Time.timeScale = 1; 
		SceneManager.LoadScene("MainMenu");
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
            bestScore = PlayerPrefs.GetInt(bestScoreSaveKey);
        }
        else
        {
            PlayerPrefs.SetInt(bestScoreSaveKey, 0);
        }
        GameOverPanel = transform.FindChild("GameOver").GetComponent<CanvasGroup>();
        GameOverPanel.alpha = 0;
        GameOverPanel.interactable = false;
        updateHUD = new UnityEvent();
        updateHUD.AddListener(UpdateHUD);
        AddPoint = new IntUnityEvent();
        AddPoint.AddListener(AddPoints);
        scoreText = GetComponentInChildren<Text>();
        heartImages = transform.FindChild("Lives").GetComponentsInChildren<Image>();

        UpdateHUD();
    }

	IEnumerator NewBestScoreAnimation(Text scoreText,Text newScoreText){
		float rad = 0;
		while (true) {
			rad += 0.05f;
			var value=Mathf.Sin (rad);
			scoreText.color = new Color (1, 1, 1, value);
			newScoreText.color = new Color (1, 1, 1, -value);
			yield return new WaitForEndOfFrame ();
		}
	}
}
