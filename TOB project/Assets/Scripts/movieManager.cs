using UnityEngine;
using System.Collections;

public class movieManager : MonoBehaviour {
	public MovieTexture movie;

	private Renderer movieRenderer;

	// Use this for initialization
	void Start () {
		// set the movie as a texture
		movieRenderer = this.GetComponent<MeshRenderer> ();
		movieRenderer.material.mainTexture = movie as MovieTexture;
		movie.Play ();
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