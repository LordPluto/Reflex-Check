using UnityEngine;
using System.Collections;

/**
 * ScoringController extends MonoBehaviour
 * 
 * Used in: Scoring object
 * 
 * public MenuController menuControl - used to reset and retry the game.
 * private float[] scores - an array holding the scores.
 * private float totalScore - the total score of the various trials.
 * public GUISkin resultSkin - the GUI Skin used for custom boxes and buttons
 * private Vector2 scrollPosition - used for the scroll bar if it's necessary
 * 
 * Overview: Displays the overall scores of the trials, as well as allowing
 * the player to either restart or retry the trials.
 * **/
public class ScoringController : MonoBehaviour {

	public MenuController menuControl;

	private float[] scores;
	private float totalScore;

	public GUISkin resultSkin;

	private Vector2 scrollPosition;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
				GUI.skin = resultSkin;
				GUI.Box (new Rect (20, 20, Screen.width - 40, Screen.height - 40), "Trial Results");
				if (GUI.Button (new Rect (70, Screen.height - 120, 100, 50), "MENU")) {
						menuControl.reset ();
						this.enabled = false;
				}
				if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height - 120, 100, 50), "RETRY")) {
						menuControl.retry ();
						this.enabled = false;
				}
				if (GUI.Button (new Rect (Screen.width - 170, Screen.height - 120, 100, 50), "EXIT")) {
						Application.Quit ();
				}

				scrollPosition = GUI.BeginScrollView (new Rect (Screen.width / 2 - 150, 150, 300, Screen.height - 320),
		                                     scrollPosition, new Rect (0, 0, 275, 40 * (scores.Length + 1)));

				for (int i = 0; i<scores.Length; i++) {
						GUI.Label (new Rect (275 / 2 - 50, 10 + 40 * i, 120, 30), "Trial " + (i + 1) + " Results: " + scores [i]);
				}
				GUI.Label (new Rect (275 / 2 - 50, 10 + 40 * scores.Length, 100, 50), "Total Score: " + totalScore + " out of " + (100 * scores.Length));

				GUI.EndScrollView ();

		}

	/**
	 * Sets up the scoring array.
	 * **/
	private void setTrialNumber(int numTrials){
				scores = new float[numTrials];
		}

	/**
	 * Copies the scores from the trial runs to this.
	 * **/
	public void setScores(float[] trialScores){
				setTrialNumber (trialScores.Length);

				scores = (float[])trialScores.Clone ();

				totalScore = 0;

				foreach (float score in scores) {
						totalScore += score;
				}
		}
}
