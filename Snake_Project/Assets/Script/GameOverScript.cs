using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public int scoreGameOver;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

       scoreGameOver = ScoreManager.currentScore;
     
        Debug.Log("Score" + scoreGameOver);

    }
}
