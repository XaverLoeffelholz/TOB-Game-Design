using UnityEngine;
using System.Collections;

public class CageScript : MonoBehaviour {
    public GameObject explosion;
    private Vector3 posOfExplosion;
    private bool cageDestroyed;

    // Use this for initialization
    void Start () {
        cageDestroyed = false;
        posOfExplosion = GameObject.Find("CenterOfCage").transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.right*2.4f * Time.deltaTime);
		transform.Rotate(Vector3.up*3.0f * Time.deltaTime, Space.World);
	}

	public void destroyCage() {
        if (cageDestroyed == false)
        {
            transform.parent.gameObject.SetActive(false);
            GameObject.Find("SparksBrokenGenerator").GetComponent<ParticleSystem>().Play();
            Instantiate(explosion, posOfExplosion, Quaternion.identity);
            cageDestroyed = true;
        }
    }
}
