using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : MonoBehaviour {

	public int damagePerTick;
	public float tickRate;
	public float duration;

	private float currentTime;
	private List<GameObject> hitObjects = new List<GameObject>();
	private ReticleTarget reticle;	// Object that is casting the firebreath

	// Use this for initialization
	void Start () {
		currentTime = 0f;
		StartCoroutine (damageTargets ());
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime >= duration) {
			Destroy (this.gameObject);
		}

		transform.LookAt (reticle.GetPointAt(50f));
	}

	// Coroutine to damage targets at a certain rate
	private IEnumerator damageTargets(){
		while (true) {
			// Check the targets hit
			foreach (GameObject hit in hitObjects) {
				var c = hit.GetComponent<Combat> ();
				if (c != null) {
					c.TakeDamage (damagePerTick);
				}
			}

			yield return new WaitForSeconds (tickRate);
		}
	}

	void OnTriggerEnter(Collider other){
		// Ignore the player
		if (other.CompareTag ("Player")) {
			return;
		}

		hitObjects.Add (other.gameObject);
	}

	void OnTriggerExit(Collider other){
		// Ignore the player
		if (other.CompareTag ("Player")) {
			return;
		}

		hitObjects.Remove (other.gameObject);
	}

	public void SetReticleTarget(ReticleTarget r){
		reticle = r;
	}

}
