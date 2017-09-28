using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

	public int punchDamage;
	public float attackRate;
	public float speed;
	public Transform rightHand;

	public Vector3 punchArea;
	public GameObject punchCollisionDetector;

	private Animator anim;
	private NavMeshAgent agent;
	private HitDetector punchHitDetector;
	private float attackCounter;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		if (anim == null) {
			Debug.Log ("Enemy Animator not found");
		}

		agent = GetComponent<NavMeshAgent> ();
		if (agent == null) {
			Debug.Log ("Enemy NavMeshAgent not found");
		}

		if (rightHand == null) {
			Debug.Log ("Enemy right hand not found");
		}

		punchHitDetector = punchCollisionDetector.GetComponent<HitDetector> ();
		if (punchHitDetector == null) {
			Debug.Log ("PunchHitDetector for enemy not found");
		}
	}
	
	// Update is called once per frame
	void Update () {
		//agent.SetDestination (target.position);
		// Animations
		if (agent.velocity.magnitude > 0) {
			anim.SetBool ("walking", true);
		} else {
			anim.SetBool ("walking", false);
		}

		// Update counters
		attackCounter += Time.deltaTime;
	}

	// Punch the target
	public void Punch(){
		// Check if it's too early to attack
		if (attackCounter < attackRate) {
			return;
		}

		// Player detected
		if (punchHitDetector.IsPlayerDetected ()) {
			anim.SetTrigger ("punch");
			attackCounter = 0;
		}
	}

	// Called by animation - check if anyone is hit by the punch
	void Punch_Hit(){
		var hits = punchHitDetector.FindDetected();
		foreach (GameObject hit in hits) {
			if (hit.CompareTag ("Player")) {
				hit.gameObject.GetComponent<Combat> ().TakeDamage (punchDamage);
			}
		}
	}

}
