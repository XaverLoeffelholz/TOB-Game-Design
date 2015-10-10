using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowScoreMainMenu : MonoBehaviour {
	public int level;
	
	// Use this for initialization
	void Start () {
		int score = 0;
		// get the score based on the level
		if (level == 1) {
			score = PlayerPrefs.GetInt ("dragonTrophy");
		} else if (level == 2) {
			score = PlayerPrefs.GetInt ("airportTrophy");
		} else if (level == 3) {
			score = PlayerPrefs.GetInt ("merlionTrophy");
		} else if (level == 4) {
			score = PlayerPrefs.GetInt ("singaTrophy");
		} else if (level == 5) {
			score = PlayerPrefs.GetInt ("mbsTrophy");
		}
		
		// 
		if (score == 0) {
			this.transform.parent.GetComponent<Image>().enabled = false;
			this.GetComponent<Text>().enabled = false;
		} else {
			this.transform.parent.GetComponent<Image>().enabled = true;
			this.GetComponent<Text>().text = score.ToString();
			this.GetComponent<Text>().enabled = true;
		}
	}
}