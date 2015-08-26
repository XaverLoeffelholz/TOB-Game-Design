using UnityEngine;
using System.Collections;

public class Initiation : MonoBehaviour {
	public GameObject platform;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				Instantiate (platform, new Vector3 (i * 2.0F, 0, j * 2.0F), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
