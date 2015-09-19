using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	public GameObject player;
	private float yValuePlayer;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		yValuePlayer = player.transform.position.y;

		if (yValuePlayer < (-40.0f)) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
