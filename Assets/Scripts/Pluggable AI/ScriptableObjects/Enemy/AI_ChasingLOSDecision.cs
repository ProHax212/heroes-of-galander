using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ChasingLookDecision")]
public class AI_ChasingLOSDecision : AI_Decision {

	public override bool Decide (AI_StateController_Abstract controller_ab)
	{
		AI_StateController controller = (AI_StateController)controller_ab;
		bool isStillInLOS = checkLOS(controller);
		return isStillInLOS;
	}

	// Check if the player is still in the line of sight of the enemy
	private bool checkLOS(AI_StateController controller){
		// Debug draw a line for enemy LOS
		Debug.DrawRay (controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.red);

		// Check if player is in line of sight
		// TODO: Check for obstacles blocking sight
		Collider[] found = Physics.OverlapSphere(controller.eyes.position, controller.enemyStats.lookRange);
		foreach (Collider col in found) {
			if(col.CompareTag("Player")){
				return true;
			}
		}

		return false;
	}

}
