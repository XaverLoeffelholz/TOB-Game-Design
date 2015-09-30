using UnityEngine;
using System.Collections;

public class UFO_Rotation : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		// rotate the UFO to make it more spectacular
        transform.Rotate(0, 15.0f * Time.deltaTime, 0 );
    }
}