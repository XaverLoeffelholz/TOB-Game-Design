using UnityEngine;
using System.Collections;

public class ScoreCalculation : MonoBehaviour {
    public float expectedTime;
    public int expectedDestroyedPlatforms;
    public int gettingTrophyBonus;

    private int finalBonus;
    private int platformBonus;
    private int timeBonus;
    private int enemyBonus;
    private int bigtrapBonus;

    private int connectionID;
    private GameObject[] enemies;

	// Use this for initialization
	void Start () {
        connectionID = 0;
        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.U))
         {
            calculateBonus();
         }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(Time.timeSinceLevelLoad);
        }
    }

    private void calculateBonus()
    {
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




    private int calculatePlatformBonus()
    {
        int destroyedPlatforms = GameObject.Find("RigidBodyFPSController").GetComponent<ShootingPlatforms>().getDestroyedPlatforms();

        if (destroyedPlatforms == expectedDestroyedPlatforms)
        {
            return 50;
        } else if (destroyedPlatforms == expectedDestroyedPlatforms-1)
        {
            return 100;
        } else if (destroyedPlatforms == expectedDestroyedPlatforms - 2)
        {
            return 200;
        } else
        {
            return 0;
        }
    }

    private int calculateTimeBonus()
    {
        float finalTime = Time.timeSinceLevelLoad; 

        if (finalTime <= 0.8* expectedTime)
        {
            return 300;
        } else if (finalTime <= 0.9 * expectedTime)
        {
            return 150;
        } else if (finalTime <= expectedTime)
        {
            return 50;
        } else
        {
            return 0;
        }

    }

    private int calculateEnemyBonus()
    {
        int killedEnemies = this.GetComponent<EnemyDeathAndRespawn>().getKilledEnemies(); ;

        if (killedEnemies == 0)
        {
            return 400;
        }
        else if (killedEnemies == 1)
        {
            return 200;
        }
        else if (killedEnemies <= 3)
        {
            return 100;
        }
        else
        {
            return 0;
        }
    }

    private int calculateBigTrapBonus()
    {
        GameObject[,] platformArray = GameObject.Find("StartPlatform").GetComponent<Initiation>().platformArray;

        for (int i = 0; i < platformArray.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < platformArray.GetLength(1) - 1; j++)
            {
                platformScript platform = platformArray[i, j].GetComponent<platformScript>();

                if (platform.getState() == "active")
                {
                    if (platform.connectionID == 0)
                    {
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
        
        for (int i = 0; i < connectionIDs.Length; i++)
        {
            
            connectionIDs[i] = getConnectionID(enemies[i]);
            Debug.Log("connectionIDs: Gegner "+ i + "hat ID: " + connectionIDs[i]);
            if (connectionIDs[i] != -1) { countConnectionIDS[connectionIDs[i]] = countConnectionIDS[connectionIDs[i]] + 1; }
           
        }

        for (int i = 0; i < countConnectionIDS.Length; i++)
        {
            Debug.Log("CountConnectionIDS, Pos" + i + " mit wert " + countConnectionIDS[i]);
        }

        int trappedEnemies = Mathf.Max(countConnectionIDS);

        if (trappedEnemies>= 3)
        {
         return trappedEnemies * 100;
        } else
        {
            return 0;
        }
        

    }

    private int getConnectionID(GameObject enemy)
    {
        RaycastHit hit;

        if (Physics.Raycast(enemy.transform.position, -Vector3.up, out hit))
        {
            if (hit.collider.CompareTag("platform"))
            {
                return hit.collider.GetComponent<platformScript>().connectionID;
            }
           
        }

        return -1;
    }
}
