using UnityEngine;
using System.Collections;

public class movieManager : MonoBehaviour {
	public MovieTexture movie;

	private Renderer movieRenderer;
	private bool stop;

	// Use this for initialization
	void Start () {
		stop = false;
		// set the movie as a texture
		movieRenderer = this.GetComponent<MeshRenderer> ();
		movieRenderer.material.mainTexture = movie as MovieTexture;
		movie.Play ();
	}	

	// Update is called once per frame
	void Update () {
		// if the Escape button is pressed, stop the cutscene
		if (Input.GetKey (KeyCode.Escape)) {
			stop = true;
		}

		// stop the cutscene if stop is true
		if (stop) {
			stopMovie ();
		}

		// if the cutscene finishes, load the main menu
		if (movie.isPlaying == false) 
		{
			Application.LoadLevel ("MainMenu");
		}

	}

	// play the cutscene
	public void playMovie () {
		Application.LoadLevel ("cutscene");
	}

	// stop the cutscene
	public void stopMovie () {
		movie.Stop ();
		// load the main menu
		Application.LoadLevel ("MainMenu");
	}
}