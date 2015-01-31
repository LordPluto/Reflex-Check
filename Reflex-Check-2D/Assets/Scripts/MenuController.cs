using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameController gameControl;

	public GUISkin menuSkin;

	#region Screen Variables

	enum MenuScreen{
		mainScreen,
		trialsScreen
	}

	private MenuScreen currentScreen;

	#endregion

	// Use this for initialization
	void Start () {
				currentScreen = MenuScreen.mainScreen;
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
				GUI.skin = menuSkin;
				GUI.Box (new Rect (20, 20, Screen.width - 40, Screen.height - 40), "Main Menu");

				switch (currentScreen) {
				case MenuScreen.mainScreen:
						if (GUI.Button (new Rect (Screen.width / 2 - 50, 125, 100, 50), "Play")) {
								currentScreen = MenuScreen.trialsScreen;
						}
						if (GUI.Button (new Rect (Screen.width / 2 - 50, 200, 100, 50), "Quit")) {
								Application.Quit ();
						}
						break;
				case MenuScreen.trialsScreen:

						break;
				default:
						break;
				}
		}

	/**
	 * Resets the menu settings back to default. Used by the scoring menu.
	 * **/
	public void reset () {
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
