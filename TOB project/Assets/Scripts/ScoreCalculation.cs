using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreCalculation : MonoBehaviour {
    public float expectedTime;
    public int expectedDestroyedPlatforms;
    public int gettingTrophyBonus;

    private GameObject[] scores;
    public GameObject ui_textGettingTheTrophy;
    public GameObject ui_textGettingTheTrophyNR;
    public GameObject ui_platformBonus;
    public GameObject ui_platformBonusNR;
    public GameObject ui_timeBonus;
    public GameObject ui_timeBonusNR;
    public GameObject ui_EnemyBonus;
    public GameObject ui_EnemyBonusNR;
    public GameObject ui_bigTrapBonus;
    public GameObject ui_bigTrapBonusNR;
    public GameObject ui_fullScore;
    public GameObject ui_fullScoreNR;

    private int finalBonus;
    private int platformBonus;
    private int timeBonus;
    private int enemyBonus;
    private int bigtrapBonus;

    private bool fadeWhite = false;
    private bool fadeScores = false;

    private int connectionID;
    private GameObject[] enemies;

    public GameObject scoreBGGameObject;
    private CanvasGroup scoreBG;

    // this is used to animate in text line after text line
    private int counter = 0;

    // Use this for initialization
    void Start() {
        connectionID = 0;
        enemies = GameObject.FindGameObjectsWithTag("enemy");

        scores = new GameObject[12];
        scores[0] = ui_textGettingTheTrophy;
        scores[1] = ui_textGettingTheTrophyNR;
        scores[2] = ui_platformBonus; 
        scores[3] = ui_platformBonusNR;
        scores[4] = ui_timeBonus;
        scores[5] = ui_timeBonusNR;
        scores[6] = ui_EnemyBonus;
        scores[7] = ui_EnemyBonusNR;
        scores[8] = ui_bigTrapBonus;
        scores[9] = ui_bigTrapBonusNR;
        scores[10] = ui_fullScore;
        scores[11] = ui_fullScoreNR;

        foreach (GameObject score in scores) {
            score.GetComponent<Text>().color = new Color(0.2f, 0.2f, 0.2f, 0.0f);
        }

        scoreBG = scoreBGGameObject.GetComponent<CanvasGroup>();
        scoreBG.alpha = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (fadeWhite) {
             scoreBG.alpha = Mathf.Lerp(scoreBG.alpha, 1.0f, Time.deltaTime);
        }
        if (fadeScores) {
           if (counter < scores.Length && animateIn(scores[counter].GetComponent<Text>())) {
                counter++;
            }
        }
    }

    private bool animateIn(Text text) {
        text.color = Color.Lerp(text.color, Color.grey, Time.deltaTime * 11.0f);
        if (text.color== Color.grey) {
            return true;
        } else {
            return false;
        }
    }

	// display the score
    public void showScore() {
        calculateBonus();
        GameObject.Find("Big Trap Bonus_number").GetComponent<Text>().text = bigtrapBonus.ToString();
        GameObject.Find("Enemy Bonus_number").GetComponent<Text>().text = enemyBonus.ToString();
        GameObject.Find("time Bonus_number").GetComponent<Text>().text = timeBonus.ToString();  
        GameObject.Find("platformBonus_number").GetComponent<Text>().text = platformBonus.ToString();
        GameObject.Find("Full Score_number").GetComponent<Text>().text = finalBonus.ToString();

        fadeinWhite();
        Invoke("fadeinScores", 1.1f);
    }

    private void fadeinWhite() {
        fadeWhite = true;
    }

    private void fadeinScores() {
        fadeScores = true;
    }

	// calculate all the bonus
    private void calculateBonus() {
        platformBonus = calculatePlatformBonus();
        timeBonus = calculateTimeBonus();
        enemyBonus = calculateEnemyBonus();
        bigtrapBonus = calculateBigTrapBonus();

        finalBonus = gettingTrophyBonus + platformBonus + timeBonus + enemyBonus + bigtrapBonus;

        Debug.Log("Das sind die Boni!");
        Debug.Log("PLatform Bonus: " + platformBonus);
        Debug.Log("timeBonus: " + timeBonus);
        Debug.Log("enemyBonus: " + enemyBonus);
        Debug.Log("bigtrapBonus :" + bigtrapBonus);
        Debug.Log("Ganzer Bonus:" + finalBonus);
    }

	// calculate the platform bonus
    private int calculatePlatformBonus() {
		// get the number of platforms destroyed
        int destroyedPlatforms = GameObject.Find("player").GetComponent<shootingPlatforms>().getDestroyedPlatforms();

		// assign score based on the number of platforms destroyed
        if (destroyedPlatforms == expectedDestroyedPlatforms) {
            return 50;
        } else if (destroyedPlatforms == expectedDestroyedPlatforms - 1) {
            return 100;
        } else if (destroyedPlatforms <= expectedDestroyedPlatforms - 2) {
            return 200;
        } else {
            return 0;
        }
    }

	// calculate the time bonus
    private int calculateTimeBonus() {
		// calculate the time taken to complete this level
        float finalTime = Time.timeSinceLevelLoad; 

		// assign score based on the time taken
        if (finalTime <= 0.8 * expectedTime) {
            return 300;
        } else if (finalTime <= 0.9 * expectedTime) {
            return 150;
        } else if (finalTime <= expectedTime) {
            return 50;
        } else {
            return 0;
        }
    }

	// calculate the enemy bonus
    private int calculateEnemyBonus() {
		// calculate the number of enemies killed
        int killedEnemies = 0;
        foreach (GameObject enemy in enemies) {
			killedEnemies += enemy.GetComponent<enemyScript>().numberOfEnemyKilled;
        }

		// assign score based on the number of enemies killed
        if (killedEnemies == 0) {
            return 400;
        } else if (killedEnemies == 1) {
            return 200;
        } else if (killedEnemies <= 3) {
            return 100;
        } else {
            return 0;
        }
    }

	// calculate the big trap bonus
    private int calculateBigTrapBonus() {
        GameObject[,] platformArray = GameObject.Find("StartPlatform").GetComponent<Initialization>().platformArray;

        for (int i = 0; i < platformArray.GetLength(0) - 1; i++) {
            for (int j = 0; j < platformArray.GetLength(1) - 1; j++) {
                platformScript platform = platformArray[i, j].GetComponent<platformScript>();

                if (platform.getState() == "active") {
                    if (platform.connectionID == 0) {
                        connectionID++;
                        platform.connectionID = connectionID;
                    } 

                    if (platformArray[i + 1, j] != null &&
                        platformArray[i + 1, j].GetComponent<platformScript>().getState() == "active")
                    {
                        platformArray[i + 1, j].GetComponent<platformScript>().connectionID = connectionID;
                    }
                    if (platformArray[i - 1, j] != null &&
                        platformArray[i - 1, j].GetComponent<platformScript>().getState() == "active")
                    {
                        platformArray[i - 1, j].GetComponent<platformScript>().connectionID = connectionID;
                    }
                    if (platformArray[i, j + 1] != null &&
                        platformArray[i, j + 1].GetComponent<platformScript>().getState() == "active")
                    {
                        platformArray[i, j + 1].GetComponent<platformScript>().connectionID = connectionID;
                    }
                    if (platformArray[i, j - 1] != null &&
                        platformArray[i, j - 1].GetComponent<platformScript>().getState() == "active")
                    {
                        platformArray[i, j - 1].GetComponent<platformScript>().connectionID = connectionID;
                    }
                }
            }
        }

        int[] connectionIDs = new int[enemies.Length];
        int[] countConnectionIDS = new int[connectionID + 1];
        for (int i = 0; i < countConnectionIDS.Length; i++) {            
            countConnectionIDS[i] = 0;
            Debug.Log("CountConnectionIDS, Pos" + i + " mit wert " + countConnectionIDS[i]);
        }

        Debug.Log("countConnectionIDS: " + countConnectionIDS.ToString());
        
        for (int i = 0; i < connectionIDs.Length; i++) {
            connectionIDs[i] = getConnectionID(enemies[i]);
            Debug.Log("connectionIDs: Gegner "+ i + "hat ID: " + connectionIDs[i]);
            if (connectionIDs[i] != -1) { 
				countConnectionIDS[connectionIDs[i]] = countConnectionIDS[connectionIDs[i]] + 1; 
			}
        }

        for (int i = 0; i < countConnectionIDS.Length; i++) {
            Debug.Log("CountConnectionIDS, Pos" + i + " mit wert " + countConnectionIDS[i]);
        }

        int trappedEnemies = Mathf.Max(countConnectionIDS);

        if (trappedEnemies >= 3) {
            return trappedEnemies * 100;
        } else {
            return 0;
        }
    }

    private int getConnectionID(GameObject enemy) {
        RaycastHit hit;
        if (Physics.Raycast(enemy.transform.position, -Vector3.up, out hit)) {
            if (hit.collider.CompareTag("platform")) {
                return hit.collider.GetComponent<platformScript>().connectionID;
            }
        }
        return -1;
    }

	// return the final score
    public int getScore() {
        return finalBonus;
    }
}