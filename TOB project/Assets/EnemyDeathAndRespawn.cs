using UnityEngine;
using System.Collections;

public class EnemyDeathAndRespawn : MonoBehaviour {
    private GameObject[] enemies;
    private int killedEnemies;

    public GameObject enemyPrefab;

    // Use this for initialization
    void Start () {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        killedEnemies = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void oneEnemyKilled(GameObject enemy)
    {
        killedEnemies++;

        // respawan
        //    GameObject newMonster = Instantiate(enemyPrefab, enemy.GetComponent<enemyScript>().getStartPosition(), Quaternion.identity) as GameObject;
        //    enemies.Add(newMonster);
    }

    public int getKilledEnemies()
    {
        return killedEnemies;
    }
}
