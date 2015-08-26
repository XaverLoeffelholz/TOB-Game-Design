using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShootingPlatforms : MonoBehaviour {

	//define maximum Distance where platforms can be shot
	public float maximumDistance; 

	Camera camera;



	private bool shootPlatform = false;
	private bool destroyPlatform = false;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("MainCamera").GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
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



		if (shootPlatform || destroyPlatform) {
			Ray ray = camera.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, maximumDistance)) {
				Debug.Log ("geht noch");
				if (hit.collider.CompareTag ("platform")) {
					if (shootPlatform) {
						createPlatformFunction (hit.collider.gameObject);
					} else {
						destroyPlatformFunction (hit.collider.gameObject);
					}
				}

			} 
		}
	
	}


	private void createPlatformFunction(GameObject platform) {
		platform.GetComponent<MeshRenderer> ().enabled = true;
	}

	private void destroyPlatformFunction(GameObject platform) {
		platform.GetComponent<MeshRenderer> ().enabled = false;
	}

}
