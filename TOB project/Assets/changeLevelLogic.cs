using UnityEngine;
using System.Collections;

public class changeLevelLogic : MonoBehaviour {

    public int mainMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // to do: open escape menu
    public void excapeMenu()
    {

    }

    // to do: call this function when back to menu is chosen in escape menu or trophy is calculated and score is shown
    public void getToMainMenu()
    {
        Application.LoadLevel(mainMenu);
    }
}
