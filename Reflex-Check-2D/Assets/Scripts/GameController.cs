using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Class GameController extends MonoBehaviour
 * 
 * Used in: _GameController object
 * 
 * public GameObject[] possibleMemoryObjects - public collection of all different memory objects.
 * private GameObject[] memoryObjectChoices - the six randomly-selected choices for this particular trial
 * private int memoryIndex - index to go with the memoryObjectChoices array
 * private float targetInstanceNumber - the instance number of the target object. Used to check if the guess was correct
 * private float spawnTimer - Timer between appearance of the different objects.
 * 
 * private int incorrectGuesses - How many guesses have been made that were wrong.
 * private float guessSpeed - Speed (in seconds) of the guess
 * private bool correctGuess - Whether or not the guess was correct.
 * private float scoreSpeed - The speed of the correct guess.
 * private int numTrials - The total number of trials.
 * private float[] trialPoints - the amount of points earned per trial.
 * private int trialIndex - Which trial is currently running.
 * 
 * Overview: Controls the trials of the guessing game. On start, randomly selects a memory object and
 * fills the possible choices. Every frame, updates the timer for spawning. Also checks to see if a
 * guess was correct when the related function is called by a MemoryObject.
 * */
public class GameController : MonoBehaviour {

	#region MemoryObjects

	public GameObject[] possibleMemoryObjects;

	private GameObject[] memoryObjectChoices;
	private int memoryIndex;

	private float targetInstanceNumber;

	private float spawnTimer = 5.0f;

	#endregion

	#region Scoring

	private int incorrectGuesses;
	private float guessSpeed;
	private bool correctGuess;

	private float scoreSpeed;

	private int numTrials = 1;
	private float[] trialPoints;
	private int trialIndex;
	
	public ScoringController resultControl;

	#endregion

	// Use this for initialization
	void Start () {
				trialIndex = -1;
				InitGame ();
				trialPoints = new float[numTrials];
		}
	
	// Update is called once per frame
	void Update () {
				spawnTimer -= Time.deltaTime;
				guessSpeed += Time.deltaTime;

				if (spawnTimer <= 0) {
						if (memoryIndex < memoryObjectChoices.Length) {
								spawnTimer = 3.0f;
								memoryObjectChoices [memoryIndex].SetActive (true);
								memoryIndex++;

								guessSpeed = 0;
						} else if (memoryIndex >= memoryObjectChoices.Length) {
								if (correctGuess) {
										trialPoints [trialIndex] = CalculateScore (scoreSpeed);
								} else {
										trialPoints [trialIndex] = 0;
								}
								if (trialIndex < numTrials - 1) {
										InitGame ();
								} else {
										resultControl.enabled = true;
					resultControl.setScores (trialPoints);
										this.enabled = false;
								}
						}
				}
		}

	/**
	 * Called by the Memory Object when one is selected.
	 * Checks to see if the instance number of the choice
	 * matches the instance number of the target.
	 * **/
	public void notifyGuess (float instanceNumber) {
				if (instanceNumber == targetInstanceNumber) {				//Guess was correct
						correctGuess = true;
						scoreSpeed = guessSpeed;
				} else {													//Guess was incorrect
						incorrectGuesses++;
						correctGuess = false;
				}
		}

	/**
	 * Initialize the guessing variables. Sets everything to beginning - allows for multiple trials.
	 * **/
	private void InitGame () {
				List<GameObject> possibleChoices = new List<GameObject> ();					//All the possible choices.
				possibleChoices.AddRange (possibleMemoryObjects);							//A List is used to facilitate removal of selections.
		
				int randomObject = Random.Range (0, possibleChoices.Count);
				GameObject targetObject = (GameObject)Instantiate (possibleChoices [randomObject]);			//Random target object
		
				MemoryObjectController targetControl = targetObject.GetComponent<MemoryObjectController> ();
				targetInstanceNumber = targetControl.getInstanceNumber ();
				targetControl.turnOffGuess ();												//Set the target number and make sure this one can't be guessed.

				targetObject.SetActive (false);
		
				possibleChoices.RemoveAt (randomObject);
		
				memoryObjectChoices = new GameObject[6];
		
				float targetPosition = Random.Range (0.0f, 6.0f);							//Random position of the target object - can be outside of choices.
		
				for (int i = 0; i<6; i++) {													//Fill in the random choices.
						if (i == (int)targetPosition) {
								memoryObjectChoices [i] = targetObject;
						} else {
								randomObject = Random.Range (0, possibleChoices.Count);
				
								GameObject chosenObject = (GameObject)Instantiate (possibleChoices [randomObject]);
								chosenObject.SetActive (false);
				
								memoryObjectChoices [i] = chosenObject;
								possibleChoices.RemoveAt (randomObject);
						}
				}
		
				memoryIndex = 0;
		
				targetObject.SetActive (true);

				incorrectGuesses = 0;
				guessSpeed = 0;
				correctGuess = false;

				scoreSpeed = 0;

				trialIndex++;																//Go to the next trial.
		}

	/**
	 * Calculates the score earned based on the amount of incorrect guesses and the speed of the correct guess.
	 * Based on a 100-point system.
	 * **/
	private float CalculateScore (float speed){
				float speedDifference = Mathf.Max (0.0f, speed - 0.25f);

				float pointsLost = ((100.0f / 1.75f) * speedDifference) * (float)(incorrectGuesses + 1);

				return Mathf.Max (0.0f, Mathf.Floor (100.0f - pointsLost));
		}
}
