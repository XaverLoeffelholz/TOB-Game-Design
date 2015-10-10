using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class platformEvent : MonoBehaviour  
{

    //IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler

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

    void OnMouseOver()
    {
        hover = true;
    }

    void Update()
    {

        if (hover)
        {
            // rotate the trophy to make it more spectacular
            trophy.GetComponent<trophyRotation>().enabled = true;
            // change the platform color to highlight color
            platformRenderer.material.color = highlightColor;

            if (Input.GetMouseButtonDown(0))
            {
                // load a level on mouse click
                Application.LoadLevel("Level" + level.ToString());
            }
        } else { 
            // stop the rotation of the trophy
            if (trophy != null)
            {
                trophy.GetComponent<trophyRotation>().enabled = false;
            }
            // change the platform color back to its original color
            platformRenderer.material.color = platformColor;
        }
    }

    void LateUpdate()
    {
        hover = false;
    }

    void onMouseExit()
    {
        // stop the rotation of the trophy
        if (trophy != null)
        {
            trophy.GetComponent<trophyRotation>().enabled = false;
        }
        // change the platform color back to its original color
        Debug.Log("In On MOuse Exit!");
        platformRenderer.material.color = platformColor;
    }


    /*

	public void OnPointerEnter(PointerEventData eventData) {
		// rotate the trophy to make it more spectacular
		trophy.GetComponent<trophyRotation> ().enabled = true;
		// change the platform color to highlight color
		platformRenderer.material.color = highlightColor;
	}

	public void OnPointerExit(PointerEventData eventData) {
		// stop the rotation of the trophy
		if (trophy != null) {
			trophy.GetComponent<trophyRotation> ().enabled = false;
		}
		// change the platform color back to its original color
		platformRenderer.material.color = platformColor;
	}

	public void OnPointerClick(PointerEventData eventData) {
        // load a level on mouse click
        Application.LoadLevel ("Level" + level.ToString ());

        /*
		// debug codes
		if (level == 1) {
			PlayerPrefs.SetInt ("dragonTrophy", 1);
		} else if (level == 2) {
			PlayerPrefs.SetInt ("airportTrophy", 1);
		} else if (level == 3) {
			PlayerPrefs.SetInt ("merlionTrophy", 1);
		} else if (level == 4) {
			PlayerPrefs.SetInt ("singaTrophy", 1);
		} else if (level == 5) {
			PlayerPrefs.SetInt ("mbsTrophy", 1);
		}
        
	}*/



}