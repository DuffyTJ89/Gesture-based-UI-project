using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public string Level1;
   // public string scores;

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
