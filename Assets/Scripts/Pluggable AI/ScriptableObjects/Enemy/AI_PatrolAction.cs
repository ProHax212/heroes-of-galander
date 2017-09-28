using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class AI_PatrolAction : AI_Action {

	// Go to the next waypoint, update waypoint once you get there
	public override void Act (AI_StateController_Abstract controller_ab)
	{
		AI_StateController controller = (AI_StateController)controller_ab;
		Patrol (controller);
	}

	// Go to the next waypoint, update waypoint once you get there
	public void Patrol(AI_StateController controller){
		controller.navMeshAgent.SetDestination (controller.waypoints [controller.nextWayPoint].position);
		controller.navMeshAgent.isStopped = false;

		// Go to the next waypoint if necessary
		if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance) {
			controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.waypoints.Count;
		}
	}

}
