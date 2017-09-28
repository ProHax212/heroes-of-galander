using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public float minY, maxY;
	public float verticalSpeed;

	private float currentY;

	// Use this for initialization
	void Start () {
		currentY = 0f;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		currentY += Input.GetAxis ("Mouse Y") * -1 * Time.deltaTime * verticalSpeed;
		currentY = Mathf.Clamp (currentY, minY, maxY);
		Quaternion rotation = Quaternion.Euler (new Vector3 (currentY, 0f, 0f));
		transform.position = target.transform.TransformPoint(rotation * offset);
		transform.LookAt (target);
	}
}
