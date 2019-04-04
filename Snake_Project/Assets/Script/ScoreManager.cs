using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{


    public static int score;
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
        if (score < 0)
        {
            score = 0;
        }

        scoretext.text = "" + score;
    }

    // static to only allow one instance of score
    public static void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        PlayerPrefs.SetInt("CurrentPlayerScore", score);
    }
    // reset points
    public static void ResetPoints()
    {
        score = 0;
    }
}