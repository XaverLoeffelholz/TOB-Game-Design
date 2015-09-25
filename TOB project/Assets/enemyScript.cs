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

    //how often did player kill this enemy:
    public int numberofDeaths = 0;

    public GameObject spawnAnimation;

	public float jumpspeed;

	private Transform goal;
	private NavMeshAgent agent;
	private bool startNav = false;
	private bool jumpDown = false;

	private Rigidbody playerBody;
	private GameObject player;
    private Vector3 startPosition;

	// Use this for initialization
	void Start () {
    }


	// give monster first goal when plattforms are created
	public void initiateGoal () {
		player = GameObject.Find ("RigidBodyFPSController");
		playerBody = player.GetComponent<Rigidbody> ();
		
		goal = GameObject.Find("MainCamera").transform;
		agent = GetComponent<NavMeshAgent>();

		if (this.GetComponent<NavMeshAgent> ().isActiveAndEnabled) {
			agent.SetDestination (goal.position);
		}

		startNav = true;
        startPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {

		goal = player.transform;
	
		if (this.GetComponent<NavMeshAgent> ().isActiveAndEnabled) {
		
			if (startNav) {
				agent.SetDestination(goal.position);
			}

			if (agent.pathStatus == NavMeshPathStatus.PathPartial && !agent.isOnOffMeshLink) {
				agent.Stop ();
				//transform.LookAt(goal);
			} else if (agent.pathStatus == NavMeshPathStatus.PathInvalid && !agent.isOnOffMeshLink) {
				agent.Stop ();
			} else {
				agent.Resume();
			}

		}



		if (!agent.isOnOffMeshLink) {
			if (Mathf.Abs(agent.velocity.x) > 0.01f || Mathf.Abs(agent.velocity.z) > 0.001f) {
				walkState = WalkingState.Running;

				//set animation speed based on navAgent 'Speed' var
				animController.speed = agent.speed;

			} else {
				walkState = WalkingState.Idle;

			}

		} else {
			walkState = WalkingState.jumpingDown;
			agent.ActivateCurrentOffMeshLink(true);

			float stepUp = 1.0f * agent.speed * Time.deltaTime;
			float stepDown = 2.0f * agent.speed * Time.deltaTime;
			Vector3 endPos = agent.currentOffMeshLinkData.endPos + Vector3.up*agent.baseOffset;
			Vector3 middlePos = agent.currentOffMeshLinkData.startPos + Vector3.up*0.6f;
			middlePos.Set(agent.currentOffMeshLinkData.startPos.x + 0.8f*(endPos.x-agent.currentOffMeshLinkData.startPos.x), middlePos.y, agent.currentOffMeshLinkData.startPos.z + 0.8f*(endPos.z-agent.currentOffMeshLinkData.startPos.z));

		//	transform.LookAt(goal);

			if (transform.position != middlePos && jumpDown == false) {
				transform.position = Vector3.MoveTowards(transform.position, middlePos, stepUp);
			} else if (transform.position.y >= (middlePos.y-0.8) && jumpDown == false) {
				jumpDown = true;
			}

            if (transform.position != endPos && jumpDown == true) {
				transform.position = Vector3.MoveTowards(transform.position, endPos, stepDown);
			} else if (transform.position.y <= (endPos.y + 0.5) && jumpDown == true) {
				jumpDown = false;
                agent.CompleteOffMeshLink();
                walkState = WalkingState.Running;
				Debug.Log ("Off mesh link fertig!");
				Debug.Log ("State: " + walkState);

			}
		}



		//get vector between monster and player
		Vector3 distanceTopPlayer = transform.position - goal.position;
		if (distanceTopPlayer.magnitude <= 2.5f) {
			animController.SetInteger ("attackState", (int)AttackState.Attack);
		}

		if (distanceTopPlayer.magnitude <= 1.6f) {
			//transform.LookAt(goal);
			Invoke("punchPlayer",0.15f);
		} else {
			animController.SetInteger ("attackState", (int)AttackState.NoAttack);
		}

		//send move state info to animator controller
		animController.SetInteger ("walkingState", (int)walkState);
		
	}

	public void punchPlayer() {
		playerBody.drag = 0.0f;
		playerBody.velocity = new Vector3 (playerBody.velocity.x, 0f, playerBody.velocity.z);
		playerBody.MovePosition (new Vector3 (player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z - 0.1f));
        playerBody.AddForce(agent.velocity * 15.0f, ForceMode.Acceleration);
    }

	public Vector3 getStartPosition()
    {
        return startPosition;
    }

    private void respawnEnemy()
    {
        Instantiate(spawnAnimation, getStartPosition(), Quaternion.identity);
        this.transform.position = getStartPosition();
        this.GetComponent<NavMeshAgent>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void kill()
    {
        numberofDeaths++;
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().mass = 25;
        //this.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, -3.0f, 0.0f), ForceMode.Acceleration);
        //this.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(-5.0f, 0.0f, 0.0f), ForceMode.Impulse);
        Invoke("respawnEnemy", 4.0f);
    }
}
