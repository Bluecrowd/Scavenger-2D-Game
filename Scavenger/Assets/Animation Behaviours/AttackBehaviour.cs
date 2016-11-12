using UnityEngine;
using System.Collections;

/**
 * @author Nick Oosterhuis 
 */ 
public class AttackBehaviour : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		animator.GetComponent<Character> ().Attack = true; 
		animator.SetFloat ("Speed", 0);

		if (animator.tag == "Player") {
			if (Player.Instance.OnGround) {
				Player.Instance.Rb2d.velocity = Vector2.zero;
			}
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Character> ().Attack = false;
		animator.GetComponent<Character> ().MeleeAttack ();
		animator.ResetTrigger ("Attack");
		animator.ResetTrigger ("Throw");
	}
}
