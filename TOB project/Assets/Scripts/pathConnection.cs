using UnityEngine;
using System.Collections;

public class pathConnection : MonoBehaviour {
	public Transform start;
	public Transform end;
	public GameObject cage;
    public bool connectionSuccessful = false;

    private GameObject[] allconnections;
    private bool allConnectionsDone = false;

    private NavMeshPath path;

	// Use this for initialization
	void Start () {
		path = new NavMeshPath ();
        allconnections = GameObject.FindGameObjectsWithTag("connectionPoint");
    }
	
	// Update is called once per frame
	void Update () {
		// check the connection from generator tower to trophy tower whenever a platform is created or destroyed
		if (Input.GetKeyUp (KeyCode.Mouse0) || Input.GetKeyUp (KeyCode.Mouse1)) {
			checkConnection();
		}
	}

	// if there is a connection between the generator tower and the trophy tower, destroy the cage
	public void checkConnection() {
		NavMesh.CalculatePath(start.position, end.position, NavMesh.AllAreas, path);
		if (path.status == NavMeshPathStatus.PathComplete) {
            connectionSuccessful = true;

            // check thorugh all connections
            allConnectionsDone = true;

            foreach (GameObject connection in allconnections)
            {
                if (!connection.GetComponent<pathConnection>().connectionSuccessful)
                {
                    allConnectionsDone = false;
                }
            }

            if (allConnectionsDone) { cage.GetComponent<cageScript>().destroyCage(); }
		} 
	}
}