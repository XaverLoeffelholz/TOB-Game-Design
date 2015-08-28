using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	public Transform goal;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<NavMeshAgent> ().isActiveAndEnabled) {
			agent.destination = goal.position; 
		}
	}
	
}
