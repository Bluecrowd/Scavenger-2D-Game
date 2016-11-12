using UnityEngine;
using System.Collections;

/**
 * @author Nick Oosterhuis 
 */ 
public class LandBehaviour : StateMachineBehaviour {

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (Player.Instance.OnGround) {
			animator.SetBool ("Land", false); 
			animator.ResetTrigger ("Jump");
		}
	}
}
