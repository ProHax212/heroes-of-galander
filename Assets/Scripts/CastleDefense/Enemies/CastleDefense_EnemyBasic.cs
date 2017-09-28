using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CastleDefense_EnemyBasic : Enemy_Abstract {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		if (anim == null) {
			Debug.Log ("Can't find animator for basic enemy");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Basic punch
	public override void Attack (GameObject target, Transform targetPos)
	{
		anim.SetTrigger ("Punch");
	}

	// Called from animation
	public void Punch(){
		// Check collision for punch and take damage
	}

	// NOT IMPLEMENTED
	public override void AbilityOne (GameObject target, Transform targetPos)
	{
		throw new System.NotImplementedException ();
	}
	public override void AbilityTwo (GameObject target, Transform targetPos)
	{
		throw new System.NotImplementedException ();
	}

}
