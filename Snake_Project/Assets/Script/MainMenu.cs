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

    public string Level1;
    // public string scores;

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void Check()
    {
        keywordActions.Add("start", NewGame);
        keywordActions.Add("scores", Scores);
        keywordActions.Add("exit", ExitGame);
        

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
    }

    // New Game
    public void NewGame()
    { // Load first level
        SceneManager.LoadScene(Level1);
      
       
    }

    // Scores
    public void Scores()
    {

        // Load scores
       // SceneManager.LoadScene(scores);
    }

    // exit Game
    public void ExitGame()
    {
        // Quit Game
        Application.Quit();
        Debug.Log("User has exited game");
    }

}
