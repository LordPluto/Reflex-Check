using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject[] possibleMemoryObjects;

	private GameObject targetObject;
	private GameObject[] memoryObjectChoices;
	private int index;

	private float targetInstanceNumber;

	private float spawnTimer = 5.0f;

	// Use this for initialization
	void Start () {
				List<GameObject> possibleChoices = new List<GameObject> ();
				possibleChoices.AddRange (possibleMemoryObjects);

				int randomObject = Random.Range (0, possibleChoices.Count - 1);

				targetObject = (GameObject)Instantiate (possibleChoices [randomObject]);

				MemoryObjectController targetControl = targetObject.GetComponent<MemoryObjectController> ();
				targetInstanceNumber = targetControl.getInstanceNumber ();
				targetControl.turnOffGuess ();

				targetObject.SetActive (false);

				possibleChoices.RemoveAt (randomObject);

				memoryObjectChoices = new GameObject[6];

				float targetPosition = Random.Range (0, 6);

				for (int i = 0; i<6; i++) {
						if (i == (int)targetPosition) {
								memoryObjectChoices [i] = targetObject;
						} else {
								randomObject = Random.Range (0, possibleChoices.Count - 1);

								GameObject chosenObject = (GameObject)Instantiate (possibleChoices [randomObject]);
								chosenObject.SetActive (false);

								memoryObjectChoices [i] = chosenObject;
								possibleChoices.RemoveAt (randomObject);
						}
				}

				index = 0;

				targetObject.SetActive (true);
		}
	
	// Update is called once per frame
	void Update () {
				spawnTimer -= Time.deltaTime;

				if (spawnTimer <= 0 && index < memoryObjectChoices.Length) {
						spawnTimer = 3.0f;
						memoryObjectChoices [index].SetActive (true);
						index++;
				}
		}

	/**
	 * Called by the Memory Object when one is selected.
	 * Checks to see if the instance number of the choice
	 * matches the instance number of the target.
	 * **/
	public void notifyGuess (float instanceNumber) {
				Debug.Log ("Your guess was: " + (instanceNumber == targetInstanceNumber ? "Correct!" : "Incorrect"));
		}
}
