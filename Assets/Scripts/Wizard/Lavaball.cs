using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lavaball : MonoBehaviour {

	public float moveSpeed;
	public float lifeTime;
	public float burnDuration;
	public GameObject fire_ring_p;
	public int impactDamage;

	private float currentLife;
	private Transform childBall, childFireParticle;	// Handle so you can destroy the child object on contact
	private float currentBurnDuration;
	private List<GameObject> targetsHit = new List<GameObject>();	// Only damage people once

	void Start(){
		childBall = transform.Find ("Ball");
		childFireParticle = transform.Find ("Fire");
	}
	
	// Update is called once per frame
	void Update () {
		// Move forward
		transform.position += transform.forward * moveSpeed * Time.deltaTime;

		// Update lifetime
		currentLife += Time.deltaTime;

		// Passed duration, explode
		if (currentLife >= lifeTime) {
			Explode ();
		}
	}

	// Explode the lavaball
	public void Explode(){
		var exp = Instantiate (fire_ring_p, transform.position, Quaternion.identity);

		// Find the ground
		RaycastHit hit;
		int layerMask = 1 << 8;
		if (Physics.Raycast(exp.transform.position, exp.transform.up*-1, out hit, Mathf.Infinity, layerMask)){
			Vector3 newPosition = hit.point;
			newPosition.y += 0.9f;
			exp.transform.position = newPosition;
		}

		Destroy (this.gameObject);
	}

	// Damage all people it goes through
	void OnTriggerEnter(Collider other){
		// Ignore collisions with player
		if (other.CompareTag ("Player")) {
			return;
		}

		if (other.gameObject.layer == LayerMask.NameToLayer ("Ground") || other.gameObject.layer == LayerMask.NameToLayer ("Environment")) {
			Explode ();
		}

		var c = other.GetComponent<Combat> ();
		if (c != null) {
			c.TakeDamage (impactDamage);
		}
	}

}
