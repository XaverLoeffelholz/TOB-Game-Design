using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class trophyScript : MonoBehaviour
{
    private Transform trophy;
    private int score = 0;
    private bool trophyCollected = false;

    // Use this for initialization
    void Start() {
        trophy = this.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update() {
		// rotate the trophy to make it more spectacular
        trophy.Rotate(0, 25.0f * Time.deltaTime, 0);

		// debug codes
        if (Input.GetKeyDown(KeyCode.U)) {
            trophyCollected = true;
            GameObject.Find("Score").GetComponent<ScoreCalculation>().showScore();
            Invoke("levelSolved", 12.0f);

            GameObject.Find("CollectTrophy").GetComponent<AudioSource>().Play();

            GameObject.Find("Score").GetComponent<ScoreCalculation>().showScore();
            Invoke("FullBonusSound", 12.5f);
        }

        if (trophyCollected) { 
         //   Time.timeScale = 0.1f;
        }
    }

	// once the trophy is collected
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            //we can not stop the time, because it would also stop the animation
            //so we block user inputs and stop the monsters
            RigidbodyFirstPersonController playerRigidBody = other.transform.GetComponent<RigidbodyFirstPersonController>();
            playerRigidBody.enabled = false;

            foreach (GameObject monster in GameObject.FindGameObjectsWithTag("enemy"))
            {
                monster.GetComponent<enemyScript>().stopHunting();
            }

            trophyCollected = true;

            GameObject.Find("CollectTrophy").GetComponent<AudioSource>().Play();

            GameObject.Find("Score").GetComponent<ScoreCalculation>().showScore();
            
            Invoke("levelSolved", 12.0f);
        }
    }

    private void levelSolved() {
        // Here we can save the score in an external file
        score = GameObject.Find("Score").GetComponent<ScoreCalculation>().getScore();
        if (Application.loadedLevelName == "Level1") { PlayerPrefs.SetInt("dragonTrophy", score); }
        else if (Application.loadedLevelName == "Level2") { PlayerPrefs.SetInt("airportTrophy", score); }
        else if (Application.loadedLevelName == "Level3") { PlayerPrefs.SetInt("merlionTrophy", score); }
        else if (Application.loadedLevelName == "Level4") { PlayerPrefs.SetInt("singaTrophy", score); }
        else if (Application.loadedLevelName == "Level5") { PlayerPrefs.SetInt("mbsTrophy", score); }

        Application.LoadLevel("MainMenu");
    }
}