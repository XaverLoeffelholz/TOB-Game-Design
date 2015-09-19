using UnityEngine;
using System.Collections;

public class pathConnections : MonoBehaviour {
	public Transform start;
	public Transform end;
	public GameObject cage;

	private NavMeshPath path;

	// Use this for initialization
	void Start () {
		path = new NavMeshPath ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Mouse0) || Input.GetKeyUp (KeyCode.Mouse1)) {
			checkConnection();
		}
	}

	public void checkConnection() {
		NavMesh.CalculatePath(start.position, end.position, NavMesh.AllAreas, path);
		if (path.status == NavMeshPathStatus.PathComplete) {
			Debug.Log ("way is complete");
			cage.GetComponent<CageScript>().destroyCage() ;
		} 
	}
}
