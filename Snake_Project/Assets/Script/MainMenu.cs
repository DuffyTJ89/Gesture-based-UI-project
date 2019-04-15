using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using System.Linq;


public class MainMenu : MonoBehaviour
{
    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;

    // Use this for initialization
    void Start()
    {
        Check();
    }

    public string Level1;
    // public string scores;

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void Check()
    {
        keywordActions.Add("new game", NewGame);
        keywordActions.Add("quit", ExitGame);
        

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
    }

    // New Game
    public void NewGame()
    { // Load first level
        SceneManager.LoadScene("Level1");
      
       
    }

    // exit Game
    public void ExitGame()
    {
        // Quit Game
        Application.Quit();
        Debug.Log("User has exited game");
    }

}
