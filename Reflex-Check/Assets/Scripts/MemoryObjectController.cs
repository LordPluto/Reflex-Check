using UnityEngine;
using System.Collections;

/**
 * Class MemoryObjectController extends MonoBehaviour
 * 
 * Used in: All MemoryObject prefabs
 * 
 * private const float TimeLimit - time limit for object to be on screen.
 * private float timeRemaining - time remaining for the object to be on screen.
 * private bool objectGuessed - determines whether or not the object has been guessed as a match.
 * public GameController gameControl - allows the object to communicate with the game controller.
 * 
 * Overview: At creation, sets the timeRemaining to the max time limit, initializes the gameControl if null,
 * and initializes the objectGuessed variable to false - i.e., the object has not been guessed yet.
 * Each call of the Update function, checks to see the object has been guessed and acts accordingly.
 * In addition, decrements the time remaining and checks to see if time is up. If time is up, destroys the object.
 * **/
public class MemoryObjectController : MonoBehaviour {
	
	private const float TimeLimit = 2.0f;
	private float timeRemaining;

	private bool objectGuessed;
	
	public GameController gameControl;

	// Use this for initialization
	void Start () {
				timeRemaining = TimeLimit;	

				objectGuessed = false;

				if (gameControl == null) {		//The programmer forgot to add the reference, silly me
						gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
				}
		}
	
	// Update is called once per frame
	void Update () {
				if (!objectGuessed && Input.GetKeyDown (KeyCode.Space)) {
						objectGuessed = true;
						//gameControl.notifyGuess(gameObjects);
				}

				timeRemaining -= Time.deltaTime;

				if (timeRemaining <= 0.0f) {
						Destroy (gameObject);
				}
		}
}
