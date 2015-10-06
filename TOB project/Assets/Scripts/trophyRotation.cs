using UnityEngine;
using System.Collections;

public class trophyRotation : MonoBehaviour {

	private float rotationSpeed = 100f;

	// Update is called once per frame
	void Update () {
		// rotate the trophy to make it more spectacular
		transform.Rotate(0, rotationSpeed * Time.deltaTime, 0 );
	}
}