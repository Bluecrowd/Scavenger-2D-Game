using UnityEngine;
using System.Collections;

/**
 * @author Nick Oosterhuis 
 */ 
public class DamageBehaviour : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Character> ().TakingDamage = true;
		animator.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Character> ().TakingDamage = false;
	}
}
