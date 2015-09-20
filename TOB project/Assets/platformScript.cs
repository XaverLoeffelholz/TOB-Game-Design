using UnityEngine;
using System.Collections;

public class platformScript : MonoBehaviour {
	private int idX;
	private int idZ;
	private Initiation init;
	private string state = "inactive";

	public Color previewColor;
	public Color activeColor;
    //public Color highlight;
    public bool indestructable = false;

    public int connectionID;

	// Use this for initialization
	void Start () {
		init = GameObject.Find ("StartPlatform").GetComponent<Initiation> ();
        connectionID = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("fixed") || other.gameObject.CompareTag("end")) {
			build ();
            this.indestructable = true;
		}
	}

	// still need to make sure plattforms at the border don't create exception
	public bool checkConnection() {
		if (init.platformArray [idX, Mathf.Min((idZ + 1),((init.platformArray.GetLength(1))-1))].GetComponent<platformScript> ().getState().Equals("active")
		    || init.platformArray [Mathf.Min((idX + 1),((init.platformArray.GetLength(0))-1)), idZ].GetComponent<platformScript> ().getState().Equals("active")
		    || init.platformArray [Mathf.Max((idX - 1),0), idZ].GetComponent<platformScript> ().getState().Equals("active")
		    || init.platformArray [idX, Mathf.Max((idZ - 1),0)].GetComponent<platformScript> ().getState().Equals("active")) {
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
				if (enemy.GetComponent<NavMeshAgent>().isActiveAndEnabled) {
					enemy.GetComponent<NavMeshAgent>().enabled = false;
					enemy.GetComponent<Rigidbody>().isKinematic = false;
                    enemy.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, -3.0f, 0.0f), ForceMode.Impulse);
                    GameObject.Find("Calculations").GetComponent<EnemyDeathAndRespawn>().oneEnemyKilled(enemy);
                }
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
		//this.GetComponent<Renderer> ().material.
		this.GetComponent<Renderer> ().material.SetColor ("_Color", previewColor);
	}

	public void unpreview() {
		state = "inactive";
		this.GetComponent<MeshRenderer> ().enabled = false;
	}
	
}
