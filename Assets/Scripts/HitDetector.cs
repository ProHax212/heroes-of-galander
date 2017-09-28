using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour {

	private List<GameObject> detectedObjects = new List<GameObject>();

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			detectedObjects.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player")) {
			detectedObjects.Remove (other.gameObject);
		}
	}

	public List<GameObject> FindDetected(){
		return detectedObjects;
	}

	public bool IsPlayerDetected(){
		foreach (GameObject obj in detectedObjects){
			if(obj.CompareTag("Player")){
				return true;
			}
		}

		return false;
	}

}
