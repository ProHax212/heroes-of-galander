using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="PluggableAI/Actions/ChaseKing")]
public class ChaseKingAction : AI_Action {

	public override void Act (AI_StateController_Abstract controller_ab)
	{
		// Convert to BasicStateController
		CastleDefense_BasicStateController controller = (CastleDefense_BasicStateController) controller_ab;

		// Chase the king
		controller.navMeshAgent.SetDestination(controller.king.transform.position);
	}

}
