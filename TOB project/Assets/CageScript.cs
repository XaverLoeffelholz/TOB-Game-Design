using UnityEngine;
using System.Collections;

public class CageScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.right*2.4f * Time.deltaTime);
		transform.Rotate(Vector3.up*3.0f * Time.deltaTime, Space.World);
	}

	public void destroyCage() {
		transform.parent.gameObject.SetActive (false);
	}
}
