using UnityEngine;
using System.Collections;

public class platformScript : MonoBehaviour {
	private int idX;
	private int idZ;
	private Initiation init;
	private string state = "inactive";

	public Color previewColor;
	public Color activeColor;

	// Use this for initialization
	void Start () {
		init = GameObject.Find ("StartPlatform").GetComponent<Initiation> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("end")) {
			build ();
		}
	}


	/*void OnCollisionEnter(Collision other) {
		Debug.Log ("etwas auf plattform");
		if (getState ().Equals ("active") && other.gameObject.CompareTag("enemy")) {

			Debug.Log ("gegner auf plattform" + this);
		}

	}*/


	// still need to make sure plattforms at the border don't create exception
	public bool checkConnection() {
		if (init.platformArray [idX, idZ + 1].GetComponent<platformScript> ().getState().Equals("active")
		 || init.platformArray [idX + 1, idZ].GetComponent<platformScript> ().getState().Equals("active")
		 || init.platformArray [idX - 1, idZ].GetComponent<platformScript> ().getState().Equals("active")
		 || init.platformArray [idX, idZ - 1].GetComponent<platformScript> ().getState().Equals("active")) {
			return true;
		} else {
			return false;
		}
	}

	public void setID(int x, int z) {
		idX = x;
		idZ = z;
	}

	public string getState() {
		return state;
	}

	public void build() {
		state = "active";
		this.GetComponent<MeshRenderer> ().enabled = true;
		this.GetComponent<BoxCollider> ().isTrigger = false;
		this.GetComponent<Renderer> ().material.SetColor ("_Color", activeColor);	
		this.GetComponent<NavMeshObstacle> ().enabled = false;
	}

	public void destroy() {
		// check for enemys on this platform

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");

		foreach (GameObject enemy in enemies) {
			if (enemy.transform.position.x >= (this.transform.position.x - 1)
			    && enemy.transform.position.x <= (this.transform.position.x + 1)
			    && enemy.transform.position.z >= (this.transform.position.z - 1)
			    && enemy.transform.position.z <= (this.transform.position.z + 1)) {

				enemy.GetComponent<NavMeshAgent>().enabled = false;
				enemy.GetComponent<Rigidbody>().isKinematic = false;
			}

		}

		state = "inactive";
		this.GetComponent<MeshRenderer> ().enabled = false;
		this.GetComponent<BoxCollider> ().isTrigger = true;
		this.GetComponent<NavMeshObstacle> ().enabled = true;
	}

	public void preview() {
		foreach (GameObject i in init.platformArray) {
			if (i.GetComponent<platformScript>().getState().Equals("preview")) {
				i.GetComponent<platformScript>().unpreview();
			}
		}
		state = "preview";
		this.GetComponent<MeshRenderer> ().enabled = true;
		this.GetComponent<Renderer> ().material.SetColor ("_Color", previewColor);
	}

	public void unpreview() {
		state = "inactive";
		this.GetComponent<MeshRenderer> ().enabled = false;
	}

}
