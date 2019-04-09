# Voice controlled Snake

The aim of this project was to develop an application with a Natural User Interface. We chose to implement Myo armband controles on the classsic mobile game Snake.

# Technologies used
  - Unity
  - Visual Studio

# Why Snake?

With Snake being so well known (everyone above a certain age has played it) we thought this would be a good opportunity to give a different take on the classic game by adding in physical gesture controles.

# Research

Initially we were looking at using the Myo Armband which was available to us through GMIT. The issues we found with this was Myo wasn't consistant for us, we even had issues with the set up of the gestures. Myo's default collection of gestures don't really map well to moving in the directions needed to play snake. There is no real natural way to get the player to move up for example. This lead us to looking into voice controle instead and we found this made a lot more sense. Getting a player to just say "up" when they want to change the direction of the snake to moving up the 2D grid is far more natural than any of the Myo gestures or any custom gesture we could think to add.

# Creating the game

We build the game of snake to work off keyboard controles first and then added the voice commands after. 

##### Building the enviroment -
The boarders are just png images which have had a Rigidbody2D on them. Every time we spawn food it is inside this area.
```
int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
```
Then we make a new version of the food.
```
Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
```
The snake itself is just a white square, the food is a blue square. When the snake collides with food we add a prefab we have made (a white square) to the body behind the head of the snake to make it longer and delete the food.

##### add to the snakes body 

```
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
```

##### When we add to the snake we also need to delete the food
```
if (coll.name.StartsWith("Food")) {
			// Get longer in next Move call
			ate = true;
            ScoreManager.AddPoints(pointsToAdd);
            // Remove the Food
            Destroy(coll.gameObject);
		} else { 	// Collided with Tail or Border
			isDead = true;
		}
```
##### ScoreManager is used to keep track of the player's points in game.

To add voice commands we used unity's built in voice recognition.
To add it to the project we go to file -> build settings -> PC, Mac & Linux -> player settings -> windows -> capabilities and tick the microphone checkbox.
Then in our code
```
using UnityEngine.Windows.Speech;
```
##### We set  up the keywordActions variable so we can call a method later when the system hear a certain word.
```
private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
private KeywordRecognizer keywordRecognizer;
``` 
##### We then map the keywords to call a method when it recognizes a word
``` 
keywordActions.Add("right", turnRight);
keywordActions.Add("left", turnLeft);
keywordActions.Add("up", turnUp);
keywordActions.Add("down", turnDown);
``` 
##### These method will change the direction of the snake 
```
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
```
##### We check if the player is dead first because you don't want them to be able to turn after they have collided with the wall or the snake tail.

### Authors: 
Kieran O'Halloran,
Thomas Duffy
