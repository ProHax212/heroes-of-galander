using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class AI_State : ScriptableObject {

	public AI_Action[] actions;
	public AI_Transition[] transitions;
	public Color sceneGizmoColor = Color.gray;

	public void UpdateState(AI_StateController_Abstract controller){
		DoActions (controller);
		CheckTransitions (controller);
	}

	private void DoActions(AI_StateController_Abstract controller){
		foreach (AI_Action a in actions) {
			a.Act (controller);
		}
	}

	private void CheckTransitions(AI_StateController_Abstract controller){
		foreach (AI_Transition t in transitions) {
			bool decisionSuccedded = t.decision.Decide (controller);

			if (decisionSuccedded) {
				controller.TransitionToState (t.trueState);
			} else {
				controller.TransitionToState (t.falseState);
			}
		}
	}

}
