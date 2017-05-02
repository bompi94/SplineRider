using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    private CanvasGroup mainPanel;
    private CanvasGroup optionsPanel;

    private bool inOptionsCanvas = false;

    [SerializeField]
    string gameLevelName;

    FadeManager fade;

    // Use this for initialization
    void Start()
    {
        mainPanel = transform.Find("MainPanel").GetComponent<CanvasGroup>();
        optionsPanel = transform.Find("OptionsPanel").GetComponent<CanvasGroup>();
        fade = GetComponentInChildren<FadeManager>();
        GoToMain();
    }

    public void GoToGame(bool difficult)
    {
        Informations.Instance.SetDifficult(difficult); 

        fade.StartFadeOut(() =>
        {
            SceneManager.LoadScene(gameLevelName);
        });
    }


    void GoToMain()
    {
        inOptionsCanvas = false;
        optionsPanel.alpha = 0f;
        mainPanel.alpha = 1f;
    }

    void GoToOptions()
    {
        inOptionsCanvas = true;
        optionsPanel.alpha = 1f;
        mainPanel.alpha = 0f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
