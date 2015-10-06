using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
	public GameObject pausePanal;

	private bool pause;

	// Use this for initialization
	void Start () {
		pause = false;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		if (pause) {
			// set the mouse cursor to visible
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			pauseGame (true);
		} else {
			// hide the mouse cursor
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			pauseGame (false);
		}
		// if the Escape button is pressed, display the pause panel
		if (Input.GetKey (KeyCode.Escape)) {
			pause = true;
		}
	}

	// pause or continue the game
	void pauseGame (bool state) {
		if (state) {
			// pause the game
			Time.timeScale = 0.0f;
		} else {
			// continue the game
			Time.timeScale = 1.0f;
		}
		// display or hide the pause panel based on the value of state
		pausePanal.SetActive (state);
	}

	// toggle the pause state
	public void togglePause () {
		pause = !pause;
	}

	// return to main menu
	public void toMainMenu () {
		Application.LoadLevel (0);
	}
}