using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using System.Linq;

public class PauseMenu : MonoBehaviour
{
    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;


    public string selectMainMenu;
    public bool isPaused;

    // Canvas containing Pause menu user interface
    public GameObject pauseMenuCanvas;

    // Use this for initialization
    void Start()
    {
        Check();
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void Check()
    {
        keywordActions.Add("pause", Pause);
        keywordActions.Add("continue", Continue);
        keywordActions.Add("main menu", ReturnMainMenu);

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // if pause menu selected display canvas and stop time
        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;

        }

        // Reverse isPaused with escape button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;
    }
    //resume play
    public void Continue()
    {
        isPaused = false;
    }
    //go to main menu
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("StartMenu");

    }

}
