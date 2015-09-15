using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	//link to Animator component
	public Animator animController;
	
	//used to set anim controller parameters
	public enum WalkingState {Idle, Walking, Running, jumpingDown};
	public enum AttackState {NoAttack, Attack};
	public WalkingState walkState;
	
	//link to NavMeshAgent component
	public NavMeshAgent navAgent;


	public float jumpspeed;

	private Transform goal;
	private NavMeshAgent agent;
	private bool startNav = false;
	private bool jumpDown = false;

	private Rigidbody playerBody;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("RigidBodyFPSController");
		playerBody = player.GetComponent<Rigidbody> ();

		goal = player.transform;
		agent = GetComponent<NavMeshAgent>();
	}


	// give monster first goal when plattforms are created
	public void initiateGoal () {
		Debug.Log ("hier ist monster :" + this.name);
		if (this.GetComponent<NavMeshAgent> ().isActiveAndEnabled) {
			agent.SetDestination (goal.position);
		}

		startNav = true;
	}
	
	// Update is called once per frame
	void Update () {
		goal = player.transform;
	
		if (this.GetComponent<NavMeshAgent> ().isActiveAndEnabled) {
		
			if (startNav) {
				agent.SetDestination(goal.position);
			}

			if (agent.pathStatus == NavMeshPathStatus.PathPartial) {
				//agent.autoBraking = true;
				agent.Stop ();
			} else if (agent.pathStatus == NavMeshPathStatus.PathInvalid) {
				//agent.autoBraking = true;
				agent.Stop ();
			} else {
				agent.Resume();
			}

		}
		//Debug.Log ("Way to player: " + navAgent.pathStatus);
		//set animation speed based on navAgent 'Speed' var
		animController.speed = agent.speed;

		//character idle/walks/runs depending on nav agents speed

		if (!agent.isOnOffMeshLink) {
			if (agent.velocity.magnitude >= 0.5f) {
				walkState = WalkingState.Running;
			} else if (agent.velocity.magnitude >= 0.01f) {
				walkState = WalkingState.Walking;
			} else {
				walkState = WalkingState.Idle;
			}
		} else {
			walkState = WalkingState.jumpingDown;
			agent.ActivateCurrentOffMeshLink(true);

			float stepUp = 1.0f * agent.speed * Time.deltaTime;
			float stepDown = 1.5f * agent.speed * Time.deltaTime;
			Vector3 endPos = agent.currentOffMeshLinkData.endPos + Vector3.up*agent.baseOffset;
			Vector3 middlePos = agent.currentOffMeshLinkData.startPos + Vector3.up*0.6f;
			middlePos.Set(agent.currentOffMeshLinkData.startPos.x + 0.8f*(endPos.x-agent.currentOffMeshLinkData.startPos.x), middlePos.y, agent.currentOffMeshLinkData.startPos.z + 0.8f*(endPos.z-agent.currentOffMeshLinkData.startPos.z));

		//	transform.LookAt(goal);

			if (transform.position != middlePos && jumpDown == false) {
				transform.position = Vector3.MoveTowards(transform.position, middlePos, stepUp);
			} else if (transform.position == middlePos && jumpDown == false) {
				jumpDown = true;
			} else if (transform.position != endPos && jumpDown == true) {
				transform.position = Vector3.MoveTowards(transform.position, endPos, stepDown);
			} else if (transform.position == endPos && jumpDown == true) {
				jumpDown = false;
				agent.CompleteOffMeshLink ();
			}
		}

		//get vector between monster and player
		Vector3 distanceTopPlayer = transform.position - goal.position;
		/*
		if (distanceTopPlayer.magnitude <= 2.1f) {
			animController.SetInteger ("attackState", (int)AttackState.Attack);
		} else {
			animController.SetInteger ("attackState", (int)AttackState.NoAttack);
		} */

		if (distanceTopPlayer.magnitude <= 1.6f) {
			transform.LookAt(goal);
			animController.SetInteger ("attackState", (int)AttackState.Attack);
			Debug.Log ("close to player!");
			//walkState = WalkingState.jumpingDown;
			//Debug.Log ("das ist der vektor: " + new Vector3(agent.velocity.x*100.0f, 25.0f,agent.velocity.z*100.0f));
			//GameObject.Find ("RigidBodyFPSController").GetComponent<Rigidbody>().AddForce(new Vector3(100.0f, 105.0f,100.0f));
			playerBody.drag = 0.0f;
			playerBody.velocity = new Vector3 (playerBody.velocity.x, 0f, playerBody.velocity.z);
			playerBody.AddForce (agent.velocity * 40.0f, ForceMode.Acceleration);
			playerBody.MovePosition (new Vector3 (player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z));
		} else {
			animController.SetInteger ("attackState", (int)AttackState.NoAttack);
		}


		//send move state info to animator controller
		animController.SetInteger ("walkingState", (int)walkState);



	}

	void OnAnimatorMove () {
		Quaternion lookRotation = new Quaternion ();

		//set the navAgent's velocity to the velocity of the animation clip currently playing
		//	navAgent.velocity = animController.deltaPosition / Time.deltaTime;

		if (walkState == WalkingState.Idle) {
			transform.LookAt(goal);
		
		} 

	}

	
}
