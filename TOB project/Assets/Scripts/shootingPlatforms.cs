using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class shootingPlatforms : MonoBehaviour {
	//define maximum Distance where platforms can be shot
	public float maximumDistance; 

	Camera cameraObject;

	private bool hitPlatform = false;
	private bool shootPlatform = false;
	private bool destroyPlatform = false;

	private GameObject currentPreview;
    private int destroyedPlatforms;

	private float timer;
	private GameObject laserGun;
	private LineRenderer laserLine;
	private float laserDisplayTime = 0.2f;
	private Color laserBuildColor = new Color(0f, 1f, 0f, 0.4f);
	private Color laserDestroyColor = new Color(1f, 0f, 0f, 0.4f);

	// Use this for initialization
	void Start () {
        destroyedPlatforms = 0;
		cameraObject = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		laserGun = GameObject.FindGameObjectWithTag ("Gun");
		laserLine = laserGun.GetComponent<LineRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		// accumulate the time and this will be used to disable the laser effect
		timer += Time.deltaTime;

		// disable the laser effect if necessary
		if (timer >= laserDisplayTime) {
			laserLine.enabled = false;
			// reset the timer
			timer = 0f;
		}

		// set up raycast to check what gameObject is being hit
		Ray ray = cameraObject.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		RaycastHit hit;
		// if the raycast hit something
		if (Physics.Raycast (ray, out hit, maximumDistance)) {
			// if the raycast hit a platform
			if (hit.collider.CompareTag ("platform")) {
				hitPlatform = true;
			} else {
				hitPlatform = false;
			}
		// if the raycast did not hit anything
		} else {
			hitPlatform = false;
		}

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

		// set the first point of the line renderer to the gun position
		laserLine.SetPosition(0, laserGun.transform.position);
       
		// if the raycast hit a platform
		if (hitPlatform) {
			string platformState = "";
			platformState = hit.collider.gameObject.GetComponent<platformScript> ().getState ();
			// get the connectivity of the platform
			bool connected = false;
			connected = hit.collider.gameObject.GetComponent<platformScript> ().checkConnection ();

			// preview the platform if not shoot or destroy platform
			if (!shootPlatform && !destroyPlatform) {
				// if the platform is in an inactive state and connected, preview it
				if ((platformState == "inactive") && connected) {
					// preview the platform
					hit.collider.gameObject.GetComponent<platformScript> ().preview ();
				}
			}
			// if shoot platform
			else if (shootPlatform) {
				// if platform is in a preview state and connected, build it
				if (connected) {
					// build the platform
					hit.collider.gameObject.GetComponent<platformScript> ().build ();

                    // show laser green
                    laserLine.material.color = laserBuildColor;
                    laserLine.enabled = true;
                    laserLine.SetPosition(1, hit.point);
                }
			}
			// if destroy platform
			else if (destroyPlatform) {
				// if platform is in an active state
				if (platformState == "active") {
					// if the platform can be destroyed, destroy it
					if (hit.collider.gameObject.GetComponent<platformScript> ().indestructable == false) {
						destroyedPlatforms++;
						// destroy the platform
						hit.collider.gameObject.GetComponent<platformScript> ().destroy ();

                        // show laser red
                        laserLine.material.color = laserDestroyColor;
                        laserLine.enabled = true;
                        laserLine.SetPosition(1, hit.point);
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