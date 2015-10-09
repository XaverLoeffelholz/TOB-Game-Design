using UnityEngine;
using System.Collections;

public class trophyScript : MonoBehaviour
{
    private Transform trophy;
    private int score = 0;
    private bool trophyCollected = false;

    // Use this for initialization
    void Start()
    {
        trophy = this.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
		// rotate the trophy to make it more spectacular
        trophy.Rotate(0, 25.0f * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.U))
        {
            trophyCollected = true;
            GameObject.Find("Score").GetComponent<ScoreCalculation>().showScore();
            Invoke("levelSolved", 12.0f);
        }

        if (trophyCollected) { 
         //   Time.timeScale = 0.1f;

        }

    }

    void OnTriggerEnter(Collider other)
    {
        
        trophyCollected = true;
        GameObject.Find("Score").GetComponent<ScoreCalculation>().showScore();
        Invoke("levelSolved", 14.5f);
    }

    private void levelSolved()
    {
        // Here we can save the score in an external file
        score = GameObject.Find("Score").GetComponent<ScoreCalculation>().getScore();
        Application.LoadLevel(0);
    }
}