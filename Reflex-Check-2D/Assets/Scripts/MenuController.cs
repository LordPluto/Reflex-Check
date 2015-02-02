using UnityEngine;
using System.Collections;

/**
 * MenuController extends MonoBehaviour
 * 
 * Used in: Menu object
 * 
 * public GameController gameControl - gives the script access to the GameController functions
 * public GUISkin menuSkin - custom skin for the graphics
 * enum MenuScreen - enum used for the switching of screens in the menu
 * private MenuScreen currentScreen - used to switch between graphical displays (either main menu or trial selection)
 * private int trialNum - number of trials currently selected
 * 
 * Overview: Used to display and control the selection of the menu options.
 * **/
public class MenuController : MonoBehaviour {

	public GameController gameControl;

	public GUISkin menuSkin;

	#region Screen Variables

	enum MenuScreen{
		mainScreen,
		trialsScreen,
		tutorialScreen
	}

	private MenuScreen currentScreen;

	#endregion

	private int trialNum;

	// Use this for initialization
	void Start () {
				currentScreen = MenuScreen.mainScreen;
				trialNum = 1;
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
				GUI.skin = menuSkin;
				GUI.Box (new Rect (20, 20, Screen.width - 40, Screen.height - 40), "Main Menu");

				switch (currentScreen) {
				case MenuScreen.mainScreen:
						if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 75, 100, 50), "Play")) {
								currentScreen = MenuScreen.trialsScreen;
						}
						if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 25, 100, 50), "Quit")) {
								Application.Quit ();
						}
						break;
				case MenuScreen.trialsScreen:
						GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 100, 100, 50), "How many trials?");
						GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "" + trialNum);
						if (trialNum > 1 && GUI.Button (new Rect (Screen.width / 2 - 175, Screen.height / 2 - 25, 100, 50), "-1")) {
								trialNum--;
						}
						if (GUI.Button (new Rect (Screen.width / 2 + 75, Screen.height / 2 - 25, 100, 50), "+1")) {
								trialNum++;
						}
						if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 75, 100, 50), "Start!")) {
								currentScreen = MenuScreen.tutorialScreen;
						}
						break;
				case MenuScreen.tutorialScreen:
						GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 125, 100, 50), "How to Play");

						GUI.Label (new Rect (Screen.width / 2 - 300, Screen.height / 2 - 50, 600, 100),
			           "An object will appear on the screen for two seconds. Press Spacebar the next time that object appears. " +
								"Points are earned based on speed - react under half a second for full points.");

						if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height / 2 + 75, 120, 50), "Understood")) {
								gameControl.enabled = true;
								gameControl.setTrialNum (trialNum);
								this.enabled = false;
						}
						break;
				default:
						break;
				}
		}

	/**
	 * Resets the menu settings back to default. Used by the scoring menu.
	 * **/
	public void reset () {
		this.enabled = true;
		Start ();
	}

	/**
	 * Restarts based on the current settings.
	 * **/
	public void retry () {
				gameControl.Restart ();
				gameControl.enabled = true;
		}
}
