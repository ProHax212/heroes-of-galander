using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ActiveState")]
public class AI_ActiveStateDecision : AI_Decision {

	public override bool Decide (AI_StateController_Abstract controller_ab)
	{
		AI_StateController controller = (AI_StateController)controller_ab;
		// Is the player active (alive)
		bool chaseTargetIsActive = controller.chaseTarget.gameObject.activeSelf;
		return chaseTargetIsActive;
	}

}
