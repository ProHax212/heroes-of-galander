using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOrb : MonoBehaviour {

	public GameObject explosion_p;

	private int orbDamage;
	private float orbSpeed;
	private float orbDuration;
	private float currentTime;
	private bool isOrbMoving;

	void Start(){
		isOrbMoving = false;
		currentTime = 0f;
		orbDuration = 1f;	// Set to above 0 so it doesn't immediately dissapear
	}
	
	// Update is called once per frame
	void Update () {
		// Only move if the orb is moving
		if (isOrbMoving) {
			transform.position += transform.forward * Time.deltaTime * orbSpeed;

			currentTime += Time.deltaTime;
			// Orb ran out of time, destroy
			if (currentTime >= orbDuration) {
				Destroy (this.gameObject);
			}
		}
	}

	// Public method to set the orb damage
	public void SetOrbDamage(int d){
		orbDamage = d;
	}

	// Public method to set the orb speed
	public void SetOrbSpeed(float s){
		orbSpeed = s;
	}

	public void SetIsOrbMoving(bool b){
		isOrbMoving = b;
	}

	public void SetOrbDuration(float d){
		orbDuration = d;
	}

	// Collided with something
	void OnTriggerEnter(Collider other){
		// Ignore if the orb isn't moving yet
		if (!isOrbMoving) {
			return;
		}

		// Ignore player
		if (other.CompareTag ("Player")) {
			return;
		}

		var c = other.GetComponent<Combat> ();

		// Other had combat, reduce health
		if (c != null) {
			c.TakeDamage (orbDamage);
		}

		var exp = Instantiate (explosion_p, transform.position, transform.rotation);
		Destroy (exp, 5f);
		Destroy (this.gameObject);
	}

}
