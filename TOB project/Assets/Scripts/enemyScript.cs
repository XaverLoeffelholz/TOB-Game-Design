using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

    public AudioClip monsterSound1;
    public AudioClip monsterSound2;

    // link to Animator component
    public Animator animController;
	
	// used to set anim controller parameters
	public enum WalkingState {Idle, Running, jumpingDown};
	public enum AttackState {NoAttack, Attack};
	public WalkingState walkState;
	
	// link to NavMeshAgent component
	public NavMeshAgent navAgent;

    // how often did player kill this enemy:
	public int numberOfEnemyKilled = 0;

    public GameObject spawnAnimation;

	public float jumpspeed;

	private Transform goal;
	private NavMeshAgent agent;
	private bool startNav = false;
	private bool jumpDown = false;

	private Rigidbody playerBody;
	private GameObject player;
    private Vector3 startPosition;
	
	// set the initial goal for the monster to go towards the player when plattforms are created
	public void initiateGoal () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerBody = player.GetComponent<Rigidbody> ();		
		agent = GetComponent<NavMeshAgent> ();

		if (this.GetComponent<NavMeshAgent> ().isActiveAndEnabled) {
			agent.SetDestination (player.transform.position);
		}

		startNav = true;
        startPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
		// the goal is to go towards the player
		goal = player.transform;
	
		// stop the enemies if there is no path to the player
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

		// if the enemies is not on the UFO
		if (!agent.isOnOffMeshLink) {
			if (Mathf.Abs(agent.velocity.x) > 0.01f || Mathf.Abs(agent.velocity.z) > 0.001f) {
				walkState = WalkingState.Running;
				//set animation speed based on navAgent 'Speed' variable
				animController.speed = agent.speed;



            } else {
				walkState = WalkingState.Idle;
			}
		// if the enemies is on the UFO
		} else {
			agent.ActivateCurrentOffMeshLink(true);



            float stepUp = 1.0f * agent.speed * Time.deltaTime;
			float stepDown = 2.0f * agent.speed * Time.deltaTime;
			Vector3 endPos = agent.currentOffMeshLinkData.endPos + Vector3.up*agent.baseOffset;
			Vector3 middlePos = agent.currentOffMeshLinkData.startPos + Vector3.up*0.6f;
			middlePos.Set(agent.currentOffMeshLinkData.startPos.x + 0.8f*(endPos.x-agent.currentOffMeshLinkData.startPos.x), middlePos.y, agent.currentOffMeshLinkData.startPos.z + 0.8f*(endPos.z-agent.currentOffMeshLinkData.startPos.z));

            //	transform.LookAt(goal);

            walkState = WalkingState.jumpingDown;

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
				Debug.Log ("Monster jumping down");
			}
		}

		// check the distance from the enemy to player and decide whether to attack or punch
		Vector3 distanceToPlayer = transform.position - goal.position;
		if (distanceToPlayer.magnitude <= 2.5f) {
			animController.SetInteger ("attackState", (int)AttackState.Attack);
		} else
        {
            animController.SetInteger("attackState", (int)AttackState.NoAttack);
        }
		if (distanceToPlayer.magnitude <= 1.6f) {
			//transform.LookAt(goal);
			Invoke("punchPlayer", 0.15f);
		}

		// send move state information to animator controller
		animController.SetInteger ("walkingState", (int)walkState);
	}

    // punch the player if the player is too near the enemy
    public void punchPlayer() {
        if (!GameObject.Find("Punch").GetComponent<AudioSource>().isPlaying) { 
            GameObject.Find("Punch").GetComponent<AudioSource>().Play();
        }

        playerBody.drag = 0.0f;
		playerBody.velocity = new Vector3 (playerBody.velocity.x, 0f, playerBody.velocity.z);
		playerBody.MovePosition (new Vector3 (player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z - 0.1f));
        
        // make sure there are no infinite values for the punch
        Vector3 strength = new Vector3(Mathf.Min(agent.velocity.x * 15.0f, 150.0f), Mathf.Min(agent.velocity.y * 15.0f, 150.0f), Mathf.Min(agent.velocity.z * 15.0f, 150.0f));
        playerBody.AddForce(strength, ForceMode.Acceleration);
    }

	// return the start position of the enemy, especially for respawning
	public Vector3 getStartPosition()
    {
        return startPosition;
    }

	// respawn the enemy if it is being killed
    private void respawnEnemy()
    {
        Instantiate(spawnAnimation, getStartPosition(), Quaternion.identity);
        this.transform.position = getStartPosition();
        this.GetComponent<NavMeshAgent>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<AudioSource>().clip = monsterSound1;
        this.GetComponent<AudioSource>().Play();
    }

	// if the enemy is being killed
    public void kill()
    {
        this.GetComponent<AudioSource>().clip = monsterSound2;
        this.GetComponent<AudioSource>().Play();
        numberOfEnemyKilled++;
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().mass = 25;
		// respawn the enemy after 4 seconds
        Invoke("respawnEnemy", 4.0f);
    }

    public void stopHunting()
    {
        this.navAgent.enabled = false;
    }
}