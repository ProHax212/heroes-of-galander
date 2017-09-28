using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleTarget : MonoBehaviour {
	
	public Image reticle;
	public Camera cam;
	public float missDistance;
	public LayerMask mask;

	void Start(){
		cam = Camera.main;
	}
	
	public Vector3 GetReticleTarget(){
		Ray ray = cam.ScreenPointToRay (reticle.rectTransform.position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, mask.value)) {
			return hit.point;
		} else {
			return ray.GetPoint(missDistance);
		}
	}

	public Vector3 GetPointAt(float distance){
		Ray ray = cam.ScreenPointToRay (reticle.rectTransform.position);
		return ray.GetPoint(distance);
	}

}
