using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_StateController_Abstract : MonoBehaviour {

	public abstract void TransitionToState (AI_State nextState);

}
