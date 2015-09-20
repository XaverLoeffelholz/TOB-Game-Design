using UnityEngine;
using System.Collections;

public class UFO_rotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 15.0f * Time.deltaTime, 0 );
    }
}
