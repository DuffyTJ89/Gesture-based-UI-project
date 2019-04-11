using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScore : MonoBehaviour
{

    public int pointsToAdd;
    public AudioSource tokenSoundEffect;

    public void Start()
    {
        tokenSoundEffect = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // only allow player pickup tokens
        if (other.GetComponent<Snake>() == null)
            return;

        ScoreManager.AddPoints(pointsToAdd);
        tokenSoundEffect.Play();
      
    }

}
