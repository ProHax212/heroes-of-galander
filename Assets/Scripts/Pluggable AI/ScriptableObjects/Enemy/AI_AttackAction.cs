using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Attack")]
public class AI_AttackAction : AI_Action {

	public override void Act (AI_StateController_Abstract controller_ab)
	{
		AI_StateController controller = (AI_StateController)controller_ab;
		Attack (controller);
	}

	// Call the attack script on the Enemy object
	private void Attack(AI_StateController controller){
		if (controller.CheckIfCountDownElapsed (controller.enemyStats.attackRate)) {
			controller.GetComponent<Enemy> ().Punch ();
		}
	}

}
