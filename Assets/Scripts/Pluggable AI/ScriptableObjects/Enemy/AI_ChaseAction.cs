using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class AI_ChaseAction : AI_Action {

	public override void Act (AI_StateController_Abstract controller_ab)
	{
		AI_StateController controller = (AI_StateController)controller_ab;
		Chase (controller);	
	}

	private void Chase(AI_StateController controller){
		// Start chasing the player
		controller.navMeshAgent.destination = controller.chaseTarget.position;
		controller.navMeshAgent.isStopped = false;
	}

}
