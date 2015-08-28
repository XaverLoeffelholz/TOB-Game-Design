using UnityEngine;
using System.Collections;

public class Initiation : MonoBehaviour {

	public GameObject platform;
	public GameObject startPlatform;
	public GameObject[,] platformArray;
	public static int levelSize = 80;

	// Use this for initialization
	void Start () {

		//create a 2-Dimensional Array for all plattforms
		platformArray = new GameObject[levelSize,levelSize];

		for (int i = 0; i < levelSize; i++) {
			for (int j = 0; j < levelSize; j++) {
				// get startplatform, no need to instantiate
				if ((i * 2.0F - levelSize == 0) && (j * 2.0F -levelSize == 0)) {
					platformArray[i,j] = startPlatform;
					platformArray[i,j].GetComponent<platformScript>().build();
				} else {
					platformArray[i,j] = Instantiate (platform, new Vector3 (i * 2.0F - levelSize, 0, j * 2.0F -levelSize), Quaternion.identity) as GameObject;
				}


				//give every plattform its ID, similar to its possition in the array
				platformArray[i,j].GetComponent<platformScript>().setID(i,j);

			}
		}




	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
