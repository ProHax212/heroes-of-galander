using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CastleDefense_BasicStateController : AI_StateController_Abstract {

	public AI_State currentState;
	public Transform eyes;
	public AI_State remainState;	// Use to remain in the current state
	public float stateSphereRadius;

	[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public int nextWayPoint;
	[HideInInspector] public Enemy_Abstract enemy;
	[HideInInspector] public float stateTimeElapsed;
	[HideInInspector] public GameObject king;	// Used for chasing the king

	private CastleDefense_GameController gameController;

	void Awake(){
		enemy = GetComponent<Enemy_Abstract> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();

		if (enemy == null) {
			Debug.Log ("Enemy component of AI_StateController not found");
		}
		if (navMeshAgent == null) {
			Debug.Log ("NavMeshAgent component of AI_StateController not found");
		}
	}

	void Start(){
		gameController = GameObject.Find ("/CastleDefenseGameController/GameController").GetComponent<CastleDefense_GameController>();
		if (gameController == null) {
			Debug.Log ("Error getting game controller");
		}
		king = gameController.king;
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

	public bool CheckIfCountDownElapsed(float duration){
		stateTimeElapsed += Time.deltaTime;
		return (stateTimeElapsed >= duration);
	}

	// Call when the state changes
	private void OnExitState(){
		stateTimeElapsed = 0;
	}

	void OnDrawGizmos(){
		Gizmos.color = currentState.sceneGizmoColor;
		Gizmos.DrawWireSphere (eyes.transform.position, stateSphereRadius);
	}

}
