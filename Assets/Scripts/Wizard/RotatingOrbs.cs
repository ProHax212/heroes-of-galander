using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingOrbs : MonoBehaviour {

	public float orbDistance;
	public float rotatingTime;
	public float orbFireDuration;
	public int orbDamage;
	public int orbHeal;
	public float orbSpeed;
	public List<GameObject> orbs = new List<GameObject>();

	private float currentTime;
	private GameObject player;
	private int nextActiveOrb;

	// Use this for initialization
	void Start () {
		currentTime = 0f;
		nextActiveOrb = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTime >= rotatingTime) {
			Heal ();
		}

		currentTime += Time.deltaTime;
	}

	// Heal the player based on the number of active orbs
	private void Heal(){
		int numOrbsLeft = orbs.Capacity - nextActiveOrb;
		var c = player.GetComponent<Combat> ();
		c.Heal (numOrbsLeft * orbHeal);

		Destroy (this.gameObject);
	}

	// Set the player that has the orbs rotating around it
	public void SetPlayer(GameObject p){
		player = p;
	}

	// Fire one of the rotating orbs
	public void Fire(Vector3 target){
		// There are no orbs to fire, return
		if (nextActiveOrb == orbs.Capacity) {
			return;
		}

		var orb = orbs [nextActiveOrb];
		nextActiveOrb += 1;

		var orbScript = orb.GetComponent<FireOrb> ();
		orbScript.SetOrbDamage (orbDamage);
		orbScript.SetOrbSpeed (orbSpeed);
		orbScript.SetIsOrbMoving (true);
		orbScript.SetOrbDuration (orbFireDuration);

		// The orb no longer has a parent
		orb.transform.SetParent(null);
		//orb.transform.rotation = player.transform.rotation;
		orb.transform.LookAt(target);
	}

}
