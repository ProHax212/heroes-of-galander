using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Abstract : MonoBehaviour {

	public abstract void Attack(GameObject target, Transform targetPos);
	public abstract void AbilityOne(GameObject target, Transform targetPos);
	public abstract void AbilityTwo(GameObject target, Transform targetPos);

	// Add more abilities as needed

}
