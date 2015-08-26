using UnityEngine;
using System.Collections;

public class platformScript : MonoBehaviour {
	private int idX;
	private int idZ;
	private Initiation init;

	// Use this for initialization
	void Start () {
		init = GameObject.Find ("StartPosition").GetComponent<Initiation> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool checkConnection() {
		if (init.platformArray [idX, idZ + 1].GetComponent<MeshRenderer> ().enabled == true
		    || init.platformArray [idX + 1, idZ].GetComponent<MeshRenderer> ().enabled == true
		    || init.platformArray [idX - 1, idZ].GetComponent<MeshRenderer> ().enabled == true
		    || init.platformArray [idX, idZ - 1].GetComponent<MeshRenderer> ().enabled == true) {
			return true;
		} else {
			return false;
		}
	}

	public void setID(int x, int z) {
		idX = x;
		idZ = z;
	}
}
