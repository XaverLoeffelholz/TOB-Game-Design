using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowScoreMainMenu : MonoBehaviour {
    public int level;

	// Use this for initialization
	void Start () {
        if (level == 1)
        {
            int score1 = PlayerPrefs.GetInt("dragonTrophy");
            if (score1 == 0)
            {
                this.transform.parent.GetComponent<Image>().enabled = false;
                this.GetComponent<Text>().enabled = false;
            }
            else
            {
                this.transform.parent.GetComponent<Image>().enabled = true;
                this.GetComponent<Text>().text = score1.ToString();
                this.GetComponent<Text>().enabled = true;
            }
        }
        else if (level == 2)
        {
            int score2 = PlayerPrefs.GetInt("airportTrophy");
            if (score2 == 0)
            {
                this.transform.parent.GetComponent<Image>().enabled = false;
                this.GetComponent<Text>().enabled = false;
            }
            else
            {
                this.transform.parent.GetComponent<Image>().enabled = true;
                this.GetComponent<Text>().text = score2.ToString();
                this.GetComponent<Text>().enabled = true;
            }
        }
        else if (level == 3)
        {
            int score3 = PlayerPrefs.GetInt("merlionTrophy");
            if (score3 == 0)
            {
                this.transform.parent.GetComponent<Image>().enabled = false;
                this.GetComponent<Text>().enabled = false;
            }
            else
            {
                this.transform.parent.GetComponent<Image>().enabled = true;
                this.GetComponent<Text>().text = score3.ToString();
                this.GetComponent<Text>().enabled = true;
            }
        }
        else if (level == 4)
        {
            int score4 = PlayerPrefs.GetInt("singaTrophy");
            if (score4 == 0)
            {
                this.transform.parent.GetComponent<Image>().enabled = false;
                this.GetComponent<Text>().enabled = false;
            }
            else
            {
                this.transform.parent.GetComponent<Image>().enabled = true;
                this.GetComponent<Text>().text = score4.ToString();
                this.GetComponent<Text>().enabled = true;
            }
        }
        else if (level == 5)
        {
            int score5 = PlayerPrefs.GetInt("mbsTrophy");
            if (score5 == 0)
            {
                this.transform.parent.GetComponent<Image>().enabled = false;
                this.GetComponent<Text>().enabled = false;
            }
            else
            {
                this.transform.parent.GetComponent<Image>().enabled = true;
                this.GetComponent<Text>().text = score5.ToString();
                this.GetComponent<Text>().enabled = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
     
    }
}
