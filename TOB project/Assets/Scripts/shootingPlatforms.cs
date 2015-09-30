using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class shootingPlatforms : MonoBehaviour {
	//define maximum Distance where platforms can be shot
	public float maximumDistance; 

	Camera cameraObject;

	private bool shootPlatform = false;
	private bool destroyPlatform = false;

	private GameObject currentPreview;
    private int destroyedPlatforms;

	// Use this for initialization
	void Start () {
        destroyedPlatforms = 0;
		cameraObject = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		// based on the mouse input, decide whether the platform is being shot or destroyed
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			shootPlatform = true;
		} 
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			shootPlatform = false;
		} 
		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			destroyPlatform = true;
		} 
		if (Input.GetKeyUp (KeyCode.Mouse1)) {
			destroyPlatform = false;
		} 

		// set up raycast to check what gameObject is being hit
		Ray ray = cameraObject.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		RaycastHit hit;
		//currentPreview.GetComponent<platformScript>().unpreview();
		if (Physics.Raycast (ray, out hit, maximumDistance)) {
			if (hit.collider.CompareTag ("platform")) {
				// get the state of the platform
				string platformState = "";
				platformState = hit.collider.gameObject.GetComponent<platformScript>().getState();
				// get the connectivity of the platform
				bool connected = false;
				connected = hit.collider.gameObject.GetComponent<platformScript>().checkConnection();

				// if the platform is inactive and connected, preview it
				if ((platformState == "inactive") && connected) {
					//currentPreview = hit.collider.gameObject;
					hit.collider.gameObject.GetComponent<platformScript>().preview();
				}
				// if platform is preview, shot and connected, build it
				else if ((platformState == "preview") && shootPlatform && connected) {
					Debug.Log ("platform built");
					hit.collider.gameObject.GetComponent<platformScript>().build();
				}
				// if platform is active and destroyed
				else if ((platformState == "active") && destroyPlatform) {
					// if the platform can be destroyed, destroy it
					if (hit.collider.gameObject.GetComponent<platformScript>().indestructable == false) {
						Debug.Log ("platform destroyed");
						destroyedPlatforms++;
						hit.collider.gameObject.GetComponent<platformScript>().destroy();  
					}
				}
			}
		} 
	}

	// get the number of destroyed platforms
    public int getDestroyedPlatforms()
    {
        return destroyedPlatforms;
    }

}
