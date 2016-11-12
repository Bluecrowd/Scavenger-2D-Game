﻿using UnityEngine;
using System.Collections;

/**
 * @author Nick Oosterhuis 
 */ 
public class SlideBehaviour : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Player.Instance.Slide = true; 
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Player.Instance.Slide = false; 
		animator.ResetTrigger ("Slide");
	}
}
