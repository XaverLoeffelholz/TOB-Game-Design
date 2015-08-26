using UnityEngine;
using System.Collections;

public class Initiation : MonoBehaviour {

	public GameObject platform;
	public GameObject[,] platformArray;
	public static int levelSize = 80;

	// Use this for initialization
	void Start () {


		platformArray = new GameObject[levelSize,levelSize];

		for (int i = 0; i < levelSize; i++) {
			for (int j = 0; j < levelSize; j++) {
				platformArray[i,j] = Instantiate (platform, new Vector3 (i * 2.0F - levelSize/2, 0, j * 2.0F -levelSize/2), Quaternion.identity) as GameObject;
				platformArray[i,j].GetComponent<platformScript>().setID(i,j);

				if ((i * 2.0F - levelSize/2 == 0) && (j * 2.0F -levelSize/2 == 0)) {
					platformArray[i,j].GetComponent<MeshRenderer> ().enabled = true;
					platformArray[i,j].GetComponent<BoxCollider> ().isTrigger = false;
				}
			}
		}




	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
