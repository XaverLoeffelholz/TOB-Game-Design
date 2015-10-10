using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class platformEvent : MonoBehaviour {
    public GameObject trophy;
	public GameObject platform;
	public int level;

	private Renderer platformRenderer;
	private Color highlightColor = new Color(0.1f, 1f, 1f, 0.6f);
	private Color platformColor;

    private bool hover = false;

	void Start () {
        // set the time scale back to 1 whenever the main menu is loaded within the game
        // this will ensure that all the subsequent scenes will be loaded properly
        Time.timeScale = 1f;
		// save the original platform color
		platformRenderer = platform.GetComponent<MeshRenderer> ();
		platformColor = platformRenderer.material.color;
	}

	// set hover to true if mouse if over the platform
    void OnMouseOver() {
        hover = true;
    }

	// stop rotating the trophy and revert the color of the platform to its original
	// color if mouse exit the platform
	void onMouseExit()
	{
		// stop the rotation of the trophy
		if (trophy != null) {
			trophy.GetComponent<trophyRotation>().enabled = false;
		}
		// change the platform color back to its original color
		platformRenderer.material.color = platformColor;
	}

    void Update() {
        if (hover) {
            // rotate the trophy to make it more spectacular
            trophy.GetComponent<trophyRotation>().enabled = true;
            // change the platform color to highlight color
            platformRenderer.material.color = highlightColor;

			// load a level on mouse click
            if (Input.GetMouseButtonDown(0)) {
                Application.LoadLevel("Level" + level.ToString());
            }
        } else { 
            // stop the rotation of the trophy
            if (trophy != null) {
                trophy.GetComponent<trophyRotation>().enabled = false;
            }
            // change the platform color back to its original color
            platformRenderer.material.color = platformColor;
        }
    }

    void LateUpdate() {
        hover = false;
    }
}