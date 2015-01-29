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
 * private GameController gameControl - allows the object to communicate with the game controller.
 * private float instanceNumber - used for instance identification (to see if the guess was correct)
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
	
	private GameController gameControl;

	private float instanceNumber;

	// Use this for initialization
	void Awake () {
				timeRemaining = TimeLimit;	

				objectGuessed = false;

				gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();

				instanceNumber = Random.Range (1000.0f, 9999999.0f);
		}
	
	// Update is called once per frame
	void Update () {
				if (!objectGuessed && Input.GetKeyDown (KeyCode.Space)) {
						objectGuessed = true;
						gameControl.notifyGuess (instanceNumber);
				}

				timeRemaining -= Time.deltaTime;

				if (timeRemaining <= 0.0f) {
			timeRemaining = 2.0f;
			objectGuessed = false;
						gameObject.SetActive (false);
				}
		}

	/**
	 * Manually sets the instance number.
	 * **/
	public void setInstanceNumber (float idNum) {
				instanceNumber = idNum;
		}

	/**
	 * Gets the instance number.
	 * **/
	public float getInstanceNumber () {
				return instanceNumber;
		}

	/**
	 * Manually turns off the ability for this object to be guessed
	 * **/
	public void turnOffGuess(){
				objectGuessed = true;
		}

	/**
	 * Manually turns on the ability for this object to be guessed
	 * **/
	public void turnOnGuess () {
				objectGuessed = false;
		}
}
