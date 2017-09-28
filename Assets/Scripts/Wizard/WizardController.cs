using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour {

	// Prefabs for abilities
	public GameObject fireball_p;
	public GameObject lavaball_p;
	public GameObject firejet_p;
	public GameObject fireJetFireField_p;
	public GameObject fireOrb_p;
	public GameObject fireBreath_p;

	public Transform rightHand, leftHand, rightFoot, leftFoot, head;
	public float shiftHeight;
	public float shiftDuration;
	public float fireJet_FireFieldRate;

	private Animator anim;
	private GameObject castedLavaball;
	private GameObject castedFireOrbs;
	private GameObject castedFireBreath;
	private float shiftCount = 0f;
	private List<GameObject> fireJets = new List<GameObject>();
	private bool fire_floating = false;
	private bool lavaball_active = false;
	private bool areOrbsSpinning = false;
	private bool isFireBreathActive = false;
	private ReticleTarget reticle;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		if (anim == null) {
			Debug.Log ("Can't find animator for player");
		}

		reticle = GetComponent<ReticleTarget> ();
		if (reticle == null) {
			Debug.Log ("Can't find reticle for player");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Left click
		if (Input.GetMouseButtonDown (0)) {
			anim.SetTrigger ("left_click");
		}

		// Right click
		if (Input.GetMouseButtonDown (1)) {
			// If the lavaball exploded already, set active to false
			if (castedLavaball == null)
				lavaball_active = false;

			// Check if there is already a lavaball
			if (!lavaball_active) {
				anim.SetTrigger ("right_click");
			} else {
				if (castedLavaball != null && castedLavaball.activeSelf) {
					castedLavaball.GetComponent<Lavaball> ().Explode ();
				}
				lavaball_active = false;
			}
		}

		// Shift
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			if (!fire_floating) {
				anim.SetTrigger ("shift");
				anim.SetBool ("floating", true);
			}
		}

		// E ability
		if (Input.GetKeyDown (KeyCode.E)) {
			if (castedFireOrbs == null) {
				areOrbsSpinning = false;
			}

			// Check if there are orbs spinning
			if (!areOrbsSpinning) {
				castedFireOrbs = Instantiate(fireOrb_p, transform.position, Quaternion.identity, transform);
				castedFireOrbs.GetComponent<RotatingOrbs> ().SetPlayer (this.gameObject);
				areOrbsSpinning = true;
			} else {
				// Orbs are spinning, fire one
				castedFireOrbs.GetComponent<RotatingOrbs>().Fire(reticle.GetReticleTarget());
			}
		}

		// Q ability
		if (Input.GetKeyDown (KeyCode.Q)) {
			// Check if there is a firebreath already
			if (castedFireBreath == null) {
				isFireBreathActive = false;
			}

			// Firebreath not active, do it
			if (!isFireBreathActive) {
				castedFireBreath = Instantiate (fireBreath_p, head.transform.position, transform.rotation, transform);
				castedFireBreath.GetComponent<FireBreath> ().SetReticleTarget(reticle);
				isFireBreathActive = true;
			}
		}

		// Floating with fire jets
		if (fire_floating) {
			shiftCount += Time.deltaTime;
			// Done floating
			if (shiftCount >= shiftDuration) {
				StopJets ();
				anim.SetBool ("floating", false);
				fire_floating = false;
				GetComponent<PlayerController> ().SetGravity (true);
			}
		}
	}

	void LateUpdate(){
		CorrectJetRotation ();
	}

	// Drop fire fields from the fire_jet
	private IEnumerator DropFireFields(){
		while (fire_floating) {
			// Find the ground
			RaycastHit hit;
			Physics.Raycast (transform.position, transform.up * -1, out hit, Mathf.Infinity, 1 << 8);
			Instantiate (fireJetFireField_p, hit.point, transform.rotation);
			yield return new WaitForSeconds (fireJet_FireFieldRate);
		}

		yield return null;
	}

	// Called from animations
	void Left_Click(){
		var fireball = GameObject.Instantiate (fireball_p, rightHand.position, transform.rotation);
		fireball.transform.LookAt (reticle.GetReticleTarget());
	}

	// Called from animations
	void Right_Click(){
		var lavaball = GameObject.Instantiate (lavaball_p, rightHand.position, transform.rotation);
		lavaball.transform.LookAt (reticle.GetReticleTarget ());
		castedLavaball = lavaball;
		lavaball_active = true;
	}

	// Called from animations
	void Shift(){
		fire_floating = true;
		shiftCount = 0f;
		GetComponent<PlayerController> ().SetGravity (false);
		StartCoroutine(ShiftGoUp());
		CreateJets ();
		StartCoroutine (DropFireFields ());
	}

	// Coroutine to make the character go up for the shift ability
	IEnumerator ShiftGoUp(){
		float time = 1f;
		float rate = 1 / time;
		Vector3 startPosition = transform.position;
		Vector3 endPosition = startPosition;
		endPosition.y += shiftHeight;
		float index = 0f;

		while (index < 1.0) {
			transform.position = Vector3.Lerp (startPosition, endPosition, index);
			index += rate * Time.deltaTime;
			yield return null;
		}

		transform.position = endPosition;
	}

	private void CreateJets (){
		var j1 = Instantiate (firejet_p, rightHand.position, firejet_p.transform.rotation);
		var j2 = Instantiate (firejet_p, leftHand.position, firejet_p.transform.rotation);
		var j3 = Instantiate (firejet_p, rightFoot.position, firejet_p.transform.rotation);
		var j4 = Instantiate (firejet_p, leftFoot.position, firejet_p.transform.rotation);

		j1.transform.SetParent (rightHand);
		j2.transform.SetParent (leftHand);
		j3.transform.SetParent (rightFoot);
		j4.transform.SetParent (leftFoot);

		fireJets.Add (j1);
		fireJets.Add (j2);
		fireJets.Add (j3);
		fireJets.Add (j4);
	}

	private void StopJets(){
		foreach (GameObject jet in fireJets) {
			ParticleSystem emit = jet.transform.Find ("FireJetParticle").GetComponent<ParticleSystem> ();
			emit.Stop ();
			Destroy(jet.gameObject, 5f);
		}

		// Clear the jets list
		fireJets.Clear();
	}

	// Correct the jet rotations to face downwards
	private void CorrectJetRotation(){
		foreach (GameObject jet in fireJets) {
			jet.transform.rotation = firejet_p.transform.rotation;
		}
	}

	public Vector3 GetReticleTarget(){
		return reticle.GetReticleTarget ();
	}

}
