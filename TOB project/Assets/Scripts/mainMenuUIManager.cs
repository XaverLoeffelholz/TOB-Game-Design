﻿using UnityEngine;
using System.Collections;

public class mainMenuUIManager : MonoBehaviour {
	public GameObject platformDragon;
	public GameObject platformAirport;
	public GameObject platformMerlion;
	public GameObject platformSinga;
	public GameObject platformMBS;

	public GameObject trophyDragon;
	public GameObject trophyAirport;
	public GameObject trophyMerlion;
	public GameObject trophySinga;
	public GameObject trophyMBS;

	public GameObject fireworksRight;
	public GameObject fireworksLeft;
	public GameObject congratsText;

	public AudioClip finishSong;

	// store which trophy is collected
	private int dragonCollected;
	private int airportCollected;
	private int merlionCollected;
	private int singaCollected;
	private int mbsCollected;

	// Use this for initialization
	void Start () {
		// disable the firework and congrats text unless level 5 is completed
		fireworksRight.SetActive (false);
		fireworksLeft.SetActive (false);
		congratsText.SetActive (false);

        // enable the dragon platform by default
        platformDragon.GetComponent<platformEvent> ().enabled = true;

		// display the trophies that are already collected
		// enable the platform based on the trophies that are already collected
		if (PlayerPrefs.GetInt ("dragonTrophy") >= 1) {
			trophyDragon.GetComponent<MeshRenderer> ().enabled = true;
			platformAirport.GetComponent<platformEvent> ().enabled = true;
		}
		if (PlayerPrefs.GetInt ("airportTrophy") >= 1) {
			trophyAirport.GetComponent<MeshRenderer> ().enabled = true;
			platformMerlion.GetComponent<platformEvent> ().enabled = true;
		}
		if (PlayerPrefs.GetInt ("merlionTrophy") >= 1) {
			trophyMerlion.GetComponent<MeshRenderer> ().enabled = true;
			platformSinga.GetComponent<platformEvent> ().enabled = true;
		}
		if (PlayerPrefs.GetInt ("singaTrophy") >= 1) {
			trophySinga.GetComponent<MeshRenderer> ().enabled = true;
			platformMBS.GetComponent<platformEvent> ().enabled = true;
		}
		if (PlayerPrefs.GetInt ("mbsTrophy") >= 1) {
			trophyMBS.GetComponent<MeshRenderer> ().enabled = true;
			// enable fireworks and congrats text once level 5 is completed
			fireworksRight.SetActive (true);
			fireworksLeft.SetActive (true);
			congratsText.SetActive (true);

			// play the finishing song once level 5 is completed
            GameObject.Find("Music").GetComponent<AudioSource>().clip = finishSong;
            GameObject.Find("Music").GetComponent<AudioSource>().Play();
        }

		// display the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

	// play the cutscene
	public void playMovie () {
		Application.LoadLevel ("cutscene");
	}

    public void ShowCredits()  {

        Application.LoadLevel("Credits");
    }

	// start a new game
	public void newGame() {
        // set all the trophy to not collected
        PlayerPrefs.SetInt ("dragonTrophy", 0);
		PlayerPrefs.SetInt ("airportTrophy", 0);
		PlayerPrefs.SetInt ("merlionTrophy", 0);
		PlayerPrefs.SetInt ("singaTrophy", 0);
		PlayerPrefs.SetInt ("mbsTrophy", 0);
		// reload the main menu to remove all the trophy
		Application.LoadLevel (Application.loadedLevel);
	}

	// exit the game
	public void exitGame () {
        Application.Quit ();
	}
}