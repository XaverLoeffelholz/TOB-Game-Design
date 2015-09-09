using UnityEngine;
using System.Collections;

public class pathConnections : MonoBehaviour {
	public Transform start;
	public Transform end;
	private NavMeshPath path;

	// Use this for initialization
	void Start () {
		path = new NavMeshPath ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			NavMesh.CalculatePath(start.position, end.position, NavMesh.AllAreas, path);
			if (path.status == NavMeshPathStatus.PathPartial) {
				Debug.Log ("way is partial");
			} else if (path.status == NavMeshPathStatus.PathComplete) {
				Debug.Log ("way is complete");
			} else {
				Debug.Log ("error");
			}
		}
	}
}
