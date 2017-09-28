using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Scan")]
public class AI_ScanDecision : AI_Decision {

	public override bool Decide (AI_StateController_Abstract controller_ab)
	{
		AI_StateController controller = (AI_StateController)controller_ab;
		bool noEnemyInSight = Scan(controller);
		return noEnemyInSight;
	}

	// Look for the player for enemyStats.searchDuration number of seconds
	// Return true if you finished the scan, return false if not done scanning yet
	private bool Scan(AI_StateController controller){
		controller.navMeshAgent.isStopped = true;
		controller.transform.Rotate (0, controller.enemyStats.searchingTurnSpeed * Time.deltaTime, 0);
		return controller.CheckIfCountDownElapsed (controller.enemyStats.searchDuration);
	}

}
