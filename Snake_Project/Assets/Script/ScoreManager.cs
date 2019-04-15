using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{


    public static int currentScore;
    Text scoretext;
   

    // Use this for initialization
    void Start()
    {
        scoretext = GetComponent<Text>();
        
        // Set score to current score
       // score = PlayerPrefs.GetInt("CurrentPlayerScore");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScore < 0)
        {
            currentScore = 0;
        }

        scoretext.text = "" + currentScore;
    }

    // static to only allow one instance of score
    public static void AddPoints(int pointsToAdd)
    {
        currentScore += pointsToAdd;
        PlayerPrefs.SetInt("CurrentPlayerScore", currentScore);
        
    }
    // reset points
    public static void ResetPoints()
    {
        currentScore = 0;
    }
}