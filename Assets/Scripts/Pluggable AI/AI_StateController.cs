using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_StateController : AI_StateController_Abstract {

	public AI_State currentState;
	public List<Transform> waypoints;	// TODO: Get this list from a game manager

	public EnemyStats enemyStats;
	public Transform eyes;
	public AI_State remainState;	// Remain in the current state

	[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public int nextWayPoint;
	[HideInInspector] public Transform chaseTarget;
	[HideInInspector] public Enemy enemy;
	[HideInInspector] public float stateTimeElapsed;

	void Awake(){
		enemy = GetComponent<Enemy> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();

		if (enemy == null) {
			Debug.Log ("Enemy component of AI_StateController not found");
		}
		if (navMeshAgent == null) {
			Debug.Log ("NavMeshAgent component of AI_StateController not found");
		}
	}

	void Update(){
		currentState.UpdateState (this);
	}
		
	public override void TransitionToState(AI_State nextState){
		if (nextState != remainState) {
			currentState = nextState;
			OnExitState ();
		}
	}

	void OnDrawGizmos(){
		if (currentState != null) {
			Gizmos.color = currentState.sceneGizmoColor;
			Gizmos.DrawWireSphere (eyes.position, enemyStats.lookSphereCastRadius);
		}
	}

	public bool CheckIfCountDownElapsed(float duration){
		stateTimeElapsed += Time.deltaTime;
		return (stateTimeElapsed >= duration);
	}

	// Call when the state changes
	private void OnExitState(){
		stateTimeElapsed = 0;
	}

}
