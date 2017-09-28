using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

	public int damage;
	public float speed;
	public float duration;
	public ParticleSystem explosion;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, duration);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	// Fireball collided with something
	void OnTriggerEnter(Collider other){
		// Ignore collisions with player
		if (other.CompareTag ("Player")) {
			return;
		}

		// Create the explosion
		var exp = Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy (exp.gameObject, exp.main.duration);

		// Check if object has Combat script
		var c = other.gameObject.GetComponent<Combat>();
		if (c != null) {
			c.TakeDamage (damage);
		}

		Destroy (this.gameObject);
	}

}
