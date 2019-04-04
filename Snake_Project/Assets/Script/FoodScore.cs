using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScore : MonoBehaviour
{

    public int pointsToAdd;
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        // only allow player pickup tokens
        if (other.GetComponent<Snake>() == null)
            return;

        ScoreManager.AddPoints(pointsToAdd);
       
        // remove token object
       // Destroy(gameObject);
    }

}
