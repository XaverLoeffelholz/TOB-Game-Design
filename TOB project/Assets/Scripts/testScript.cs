using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("removeObstacle", 5); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void removeObstacle() {
		this.GetComponent<NavMeshObstacle> ().enabled = false;
		this.GetComponent<BoxCollider> ().enabled = false;
		this.GetComponent<MeshRenderer> ().enabled = false;
	}
}
