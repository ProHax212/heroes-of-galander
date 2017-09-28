using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Vector3 gizmoPosition = transform.position;
		gizmoPosition.y += 1f;
		Gizmos.DrawIcon (gizmoPosition, "spawn_point");
	}

}
