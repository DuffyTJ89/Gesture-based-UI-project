using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public int scoreText;

    // Use this for initialization
    void Start () {

        scoreText = PlayerPrefs.GetInt("CurrentPlayerScore");


    }
	
	// Update is called once per frame
	void Update () {

       
     
        Debug.Log("Score :" + scoreText);

    }
}
