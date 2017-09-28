using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class AI_LookDecision : AI_Decision {

	public override bool Decide (AI_StateController_Abstract controller_ab)
	{
		AI_StateController controller = (AI_StateController)controller_ab;
		bool targetVisible = Look (controller);
		return targetVisible;
	}

	// Look for the player
	private bool Look(AI_StateController controller){
		// Spherecast down a ray
		RaycastHit hit;

		// Debug draw a line for enemy LOS
		Debug.DrawRay (controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.green);

		// Check if the player is hit by the spherecast
		if (Physics.SphereCast (controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.lookRange)
		    && hit.collider.CompareTag ("Player")) {
			controller.chaseTarget = hit.transform;
			return true;
		} else {
			return false;
		}
	}

}
