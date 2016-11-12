using UnityEngine;
using System.Collections;

/**
 * @author Nick Oosterhuis 
 */ 
public class DeathBehaviour : StateMachineBehaviour {

	private float respawnTime = 5; 
	private float deathTimer; 

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		deathTimer = 0; 
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		deathTimer += Time.deltaTime; 

		if (deathTimer >= respawnTime) {
			animator.GetComponent<Character>().Death ();
		}
	}
}
