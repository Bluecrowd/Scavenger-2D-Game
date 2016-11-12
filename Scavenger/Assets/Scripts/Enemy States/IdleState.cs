using UnityEngine;
using System.Collections;
/**
 * describes the idle state of the enemy and sets a time which the enemy is allowed to be in idle state before it needs to move again
 * @author Nick Oosterhuis 
 */ 
public class IdleState : IEnemyState {

	private Enemy enemy; 
	private float idleTimer; 
	private float idleDuration = 5; 

	public void Execute ()
	{
		Idle ();

		if (enemy.Target != null) {
			enemy.ChangeState (new PatrolState());
		}
	}

	public void Enter (Enemy enemy)
	{
		this.enemy = enemy; 
	}

	public void Exit ()
	{
		
	}

	public void OntriggerEnter (Collider2D other)
	{
		
	}

	private void Idle() {
		enemy.Anim.SetFloat ("Speed", 0);
		idleTimer += Time.deltaTime; 

		if (idleTimer >= idleDuration) {
			enemy.ChangeState (new PatrolState());
		}
	}



}
