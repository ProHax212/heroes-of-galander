using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
	
	public float speed;
	public float rotationSpeed;
	public float jumpHeight;
	public float gravity;

	private CharacterController controller;
	private Animator anim;
	private float moveY = 0f;
	private bool isGravity;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		isGravity = true;
	}

	// Update is called once per frame
	void Update () {
		// Get the user input
		var moveX = Input.GetAxis("Horizontal");
		var moveZ = Input.GetAxis ("Vertical");
		var mouseX = Input.GetAxis ("Mouse X");

		// Update the speeds
		Vector3 movement = new Vector3(moveX, 0f, moveZ);
		movement.Normalize ();
		movement.x = speed * moveX;
		movement.z = speed * moveZ;

		// Jump
		if(controller.isGrounded){
			if (Input.GetButtonDown ("Jump")) {
				anim.SetTrigger ("jump");
			}
		}

		if (isGravity) {
			moveY -= gravity * Time.deltaTime;
			movement.y = moveY;
		} else {
			moveY = 0f;
		}

		// Move the player
		controller.Move(transform.TransformVector(movement * Time.deltaTime));
		transform.RotateAround (transform.TransformPoint (Vector3.zero), Vector3.up, mouseX * rotationSpeed * Time.deltaTime);

		// Animations
		if (controller.isGrounded) {
			if (moveX != 0 || moveZ != 0) {
				anim.SetBool ("walking", true);
			} else {
				anim.SetBool ("walking", false);
			}
		}
	}

	// Called from animations
	void Jump(){
		moveY = jumpHeight;
	}

	public void SetGravity(bool grav){
		isGravity = grav;
	}

}
