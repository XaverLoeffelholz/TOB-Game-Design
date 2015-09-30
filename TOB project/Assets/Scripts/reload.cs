using UnityEngine;
using System.Collections;

public class reload : MonoBehaviour {
	public GameObject player;
	private float yValuePlayer;
	
	// Update is called once per frame
	void Update () {
		// reload the level if the player fell off the platform (indicated by his y position of -40)
		yValuePlayer = player.transform.position.y;

		if (yValuePlayer < (-40.0f)) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}