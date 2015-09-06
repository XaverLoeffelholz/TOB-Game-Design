﻿using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	private Transform goal;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		goal = GameObject.Find ("RigidBodyFPSController").transform;
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<NavMeshAgent> ().isActiveAndEnabled) {
			agent.SetDestination(goal.position);
		}
	}
	
}
