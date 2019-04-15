# Voice controlled Snake

The aim of this project was to develop an application with a Natural User Interface. We chose to implement Myo armband controles on the classsic mobile game Snake.

# To run the game

We included an exe file in the folder SnakeBuild called Snake_Project.exe. If you double click on this on a Windows machine the game should run. We recommend using a microphone when running the game because although all the menus very responsive the gameplay itself can be unresponsive when it is unsure of the word. We tried reducing the confidence level but the issue continued. 

alternatively if you have Unity you can open the project there, we built the game using Unity 2018.2.10f1

# Technologies used
  - Unity
  - Visual Studio

# Why Snake?

With Snake being so well known (everyone above a certain age has played it) we thought this would be a good opportunity to give a different take on the classic game by adding in physical gesture controles.

# Research

Initially we were looking at using the Myo Armband which was available to us through GMIT. The issues we found with this was Myo wasn't consistant for us, we even had issues with the set up of the gestures. Myo's default collection of gestures don't really map well to moving in the directions needed to play snake. There is no real natural way to get the player to move up for example. This lead us to looking into voice controle instead and we found this made a lot more sense. Getting a player to just say "up" when they want to change the direction of the snake to moving up the 2D grid is far more natural than any of the Myo gestures or any custom gesture we could think to add.

# Creating the game

We build the game of snake to work off keyboard controles first and then added the voice commands after. 

We try to use what we think are the most natural commands in each situation in the game so for example to get the snake to turn upwards we use the command "up". 

Anywhere where we felt there might be slight confusion as to what command to give we print text onto the screen to guide the user. Below is a list of all the voice commands in the game and their purpose.
#### &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Main Menu
| Command | Action |
| ------ | ------ |
| "New game" | starts a new game  |
| "Quit" | quits the application  |
#### &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  In Game
| Command | Action |
| ------ | ------ |
| "Up" | changes the direction the snake is moving in to an upwards direction  |
| "Down" | changes the direction the snake is moving in to a downwards direction |
| "Left" | changes the direction the snake is moving in to left moving |
| "Right" | changes the direction the snake is moving in to right moving |
| "Pause" | this will trigger the pause menu, saying it again will untrigger it |
| "continue" | in the pause menu this will continue the game |
| "Main menu" | in the pause menu this bring you back to the first menu |
#### &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Game Over
| Command | Action |
| ------ | ------ |
| "New game" | starts a new game  |
| "Quit" | quits the application  |

# UI

#### Our main menu 

![alt text](https://raw.githubusercontent.com/DuffyTJ89/Gesture-based-UI-project/master/img/Capture.PNG "Logo Title Text 1")

#### the in game pause menu 


![alt text](https://raw.githubusercontent.com/DuffyTJ89/Gesture-based-UI-project/master/img/Capture2.PNG "Logo Title Text 1")

#### Game over screen

![alt text](https://raw.githubusercontent.com/DuffyTJ89/Gesture-based-UI-project/master/img/Capture3.PNG "Logo Title Text 1")

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

When we add to the snake we also need to delete the food
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
ScoreManager is used to keep track of the player's points in game.

To add voice commands we used unity's built in voice recognition.
To add it to the project we go to file -> build settings -> PC, Mac & Linux -> player settings -> windows -> capabilities and tick the microphone checkbox.
Then in our code
```
using UnityEngine.Windows.Speech;
```
We set  up the keywordActions variable so we can call a method later when the system hear a certain word.
```
private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
private KeywordRecognizer keywordRecognizer;
``` 
We then map the keywords to call a method when it recognizes a word
``` 
keywordActions.Add("right", turnRight);
keywordActions.Add("left", turnLeft);
keywordActions.Add("up", turnUp);
keywordActions.Add("down", turnDown);
``` 
These method will change the direction of the snake 
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
We check if the player is dead first because you don't want them to be able to turn after they have collided with the wall or the snake tail.

The voice commands for our menus work like the voice commands for the snake but instead we call new scenes.
``` 
keywordActions.Add("new game", NewGame);
keywordActions.Add("scores", Scores);
keywordActions.Add("exit", ExitGame);
        
keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
keywordRecognizer.Start();
``` 
 You can see the command for newGame above. That calls this method to bring up a new scene.
```
 public void NewGame()
    { // Load first level
        SceneManager.LoadScene("Level1");
    }
```
# Conclusions & Recommendations

This was an enjoyable project where we got to see some technology that we hadn't seen before like the Myo armbands and using voice control in our applications. If we were doing this project again I think we would go with a game which doesn't rely on reaction input as much as snake because the delay with voice commands makes it a worse expierence than playing with button controles.

Our menu voice commands seem to work the most issue free so we would probably look to build something like Xs and 0s or sudoku which are games where the response time isn't critical.

# Video

https://www.youtube.com/watch?v=hTCZ0SaI_Gg

### Authors: 
Kieran O'Halloran,
Thomas Duffy
