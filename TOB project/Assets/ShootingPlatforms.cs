using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShootingPlatforms : MonoBehaviour {

	//define maximum Distance where platforms can be shot
	public float maximumDistance; 

	Camera cameraObject;

	private bool shootPlatform = false;
	private bool destroyPlatform = false;

	private GameObject currentPreview;
    private int destroyedPlattforms;

	// Use this for initialization
	void Start () {
        destroyedPlattforms = 0;
        cameraObject = GameObject.Find ("MainCamera").GetComponent<Camera> ();
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


		Ray ray = cameraObject.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		RaycastHit hit;

		//currentPreview.GetComponent<platformScript>().unpreview();

		if (Physics.Raycast (ray, out hit, maximumDistance)) {
			if (hit.collider.CompareTag ("platform")) {
				if (hit.collider.gameObject.GetComponent<platformScript>().getState().Equals("inactive")
					&& hit.collider.gameObject.GetComponent<platformScript>().checkConnection() == true) {
					//currentPreview = hit.collider.gameObject;
					hit.collider.gameObject.GetComponent<platformScript>().preview();
				} 

				if (shootPlatform && hit.collider.gameObject.GetComponent<platformScript>().checkConnection() == true) {
					hit.collider.gameObject.GetComponent<platformScript>().build();
				} else if (destroyPlatform) {
                    if (hit.collider.gameObject.GetComponent<platformScript>().indestructable == false)
                    {
                        destroyedPlattforms++;
                        hit.collider.gameObject.GetComponent<platformScript>().destroy();  
                    }
                }


			}

		} 
	
	}

    public int getDestroyedPlatforms()
    {
        return destroyedPlattforms;
    }

}
