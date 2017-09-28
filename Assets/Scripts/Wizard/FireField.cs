using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireField : MonoBehaviour {

	public int burnDamage;
	public float burnRate;
	public float duration;
	private List<GameObject> inField = new List<GameObject>();
	
	void Start(){
		StartCoroutine (Burn ());
		Destroy (this.gameObject, duration);
	}

	void OnTriggerEnter(Collider other){
		// Add to the list of effected people
		inField.Add(other.gameObject);
	}

	void OnTriggerExit(Collider other){
		inField.Remove (other.gameObject);
	}

	private IEnumerator Burn(){
		while (true) {
			foreach (GameObject target in inField) {
				Combat c = target.GetComponent<Combat> ();
				if(c != null){
					if (!target.CompareTag ("Player")) {
						c.TakeDamage (burnDamage);
					}
				}
			}
			yield return new WaitForSeconds(burnRate);
		}
	}

}
