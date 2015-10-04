using UnityEngine;
using System.Collections;

public class platformScript : MonoBehaviour {
	private int idX;
	private int idZ;
	private Initialization init;
	private string state = "inactive";

	public Color previewColor;
	public Color activeColor;
    //public Color highlight;
    public bool indestructable = false;

    public GameObject destroyAnimation;
    public GameObject buildingAnimation;

    public int connectionID;

	// Use this for initialization
	void Start () {
		init = GameObject.Find ("startPlatform").GetComponent<Initialization> ();
        connectionID = 0;
    }

	// display platforms if platforms already collided with GameObject of "fixed" or "end" tag
	// furthermore, make these platforms indestructable so they cannot be destroyed
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("fixed") || other.gameObject.CompareTag("end")) {
			build ();
            this.indestructable = true;
		}
	}

	// check if there is a connecting platform
	// make sure platform at the border do not create exception
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

	// set the ID for the platform
	public void setID(int x, int z) {
		idX = x;
		idZ = z;
	}

	// get the state of the platform
	public string getState() {
		return state;
	}

	// build a platform and make it active (set the color to active clor)
	public void build() {
		// run the platform built animation
        Instantiate(buildingAnimation, this.transform.position, Quaternion.identity);
        state = "active";
		this.GetComponent<MeshRenderer> ().enabled = true;
		this.GetComponent<BoxCollider> ().isTrigger = false;
		this.GetComponent<Renderer> ().material.SetColor ("_Color", activeColor);	
		this.GetComponent<NavMeshObstacle> ().enabled = false;
	}

	// destroy a platform and make it inactive
	public void destroy() {
		// run the platorm destroyed animation
        Instantiate(destroyAnimation, this.transform.position, Quaternion.identity);

        // check for enemies on this platform and kill them
        GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");
		foreach (GameObject enemy in enemies) {
			if (enemy.transform.position.x >= (this.transform.position.x - 1)
			    && enemy.transform.position.x <= (this.transform.position.x + 1)
			    && enemy.transform.position.z >= (this.transform.position.z - 1)
			    && enemy.transform.position.z <= (this.transform.position.z + 1)) {
				if (enemy.GetComponent<NavMeshAgent>().isActiveAndEnabled) {
                    enemy.GetComponent<enemyScript>().kill();
                }
			}
		}

		// set the platform to inactive
		state = "inactive";
		this.GetComponent<MeshRenderer> ().enabled = false;
		this.GetComponent<BoxCollider> ().isTrigger = true;
		this.GetComponent<NavMeshObstacle> ().enabled = true;
	}

	// preview a platform if the cursor is on it
	public void preview() {
		// set all the platforms with a state of preview to inactive
		foreach (GameObject i in init.platformArray) {
			if (i.GetComponent<platformScript>().getState().Equals("preview")) {
				i.GetComponent<platformScript>().unpreview();
			}
		}

		// set the platform to preview (enable it and set the color to preview color)
		state = "preview";
		this.GetComponent<MeshRenderer> ().enabled = true;
		//this.GetComponent<Renderer> ().material.
		this.GetComponent<Renderer> ().material.SetColor ("_Color", previewColor);
	}

	// unpreview a platform once the cursor leaves
	public void unpreview() {
		state = "inactive";
		this.GetComponent<MeshRenderer> ().enabled = false;
	}
	
}