using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour {

    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;
    public AudioSource tokenSoundEffect;
    //public ConfidenceLevel confidence = ConfidenceLevel.Low;


    // Did the snake eat something?
    bool ate = false;

    public int pointsToAdd;
    public string StartMenu;

    //Did user died?
    public bool isDead = false;

	// Tail Prefab
	public GameObject tailPrefab;
    

    // Current Movement Direction
    // (by default it moves to the right)
    Vector2 dir = Vector2.right;

	// Keep Track of Tail
	List<Transform> tail = new List<Transform>();
    public string GameOverScene;

    // Use this for initialization
    void Start () {
		// Move the Snake every 300ms
		InvokeRepeating("Move", 0.3f, 0.3f);
        Check();
        tokenSoundEffect = GetComponent<AudioSource>();
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void Check()
    {
        keywordActions.Add("right", turnRight);
        keywordActions.Add("left", turnLeft);
        keywordActions.Add("up", turnUp);
        keywordActions.Add("down", turnDown);
        keywordActions.Add("main menu", mainMenu);

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
    }

    

    /// <summary>
    /// Moves
    /// </summary>
    private void turnDown()
    {
        if (!isDead) { dir = -Vector2.up; };
    }

    private void turnUp()
    {
        if (!isDead) { dir = Vector2.up; };
    }

    private void turnLeft()
    {
        if (!isDead) { dir = -Vector2.right; };
    }

    private void turnRight()
    {
        if (!isDead) { dir = Vector2.right; }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(StartMenu);
    }

    // Update is called once per frame
    void Update () {

    
        
        if (!isDead) {
			// Move in a new Direction?
			if (Input.GetKey (KeyCode.RightArrow))
				dir = Vector2.right;
			else if (Input.GetKey (KeyCode.DownArrow))
				dir = -Vector2.up;    // '-up' means 'down'
			else if (Input.GetKey (KeyCode.LeftArrow))
				dir = -Vector2.right; // '-right' means 'left'
			else if (Input.GetKey (KeyCode.UpArrow))
				dir = Vector2.up;
		
		}
	}

	void Move() {
		if (!isDead) {
			// Save current position (gap will be here)
			Vector2 v = transform.position;

			// Move head into new direction (now there is a gap)
			transform.Translate (dir);

			// Ate something? Then insert new Element into gap
			if (ate) {
				// Load Prefab into the world
				GameObject g = (GameObject)Instantiate (tailPrefab,
					              v,
					              Quaternion.identity);

				// Keep track of it in our tail list
				tail.Insert (0, g.transform);

				// Reset the flag
				ate = false;
			} else if (tail.Count > 0) {	// Do we have a Tail?
					// Move last Tail Element to where the Head was
					tail.Last ().position = v;

					// Add to front of list, remove from the back
					tail.Insert (0, tail.Last ());
					tail.RemoveAt (tail.Count - 1);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		// Food?
		if (coll.name.StartsWith("Food")) {
			// Get longer in next Move call
			ate = true;
            ScoreManager.AddPoints(pointsToAdd);
            // Remove the Food
            Destroy(coll.gameObject);
            tokenSoundEffect.Play();     // if pause menu selected display canvas and stop time
           
        } else { 	// Collided with Tail or Border
			isDead = true;
            Debug.Log("Game over");

            // if pause menu selected display canvas and stop time
            SceneManager.LoadScene("GameOverScene");

            Time.timeScale = 0f;
                      
        }
	}
}
 