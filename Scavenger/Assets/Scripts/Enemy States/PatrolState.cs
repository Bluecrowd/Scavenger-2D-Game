using UnityEngine;
using System.Collections;

/**
 * Describes the patrol state of the enemy. it can patrol for a certain amount of time before it needs to go back to idle. 
 * if the patrol point (edge) is found it needs to change the direction and move back to another point. 
 * @author Nick Oosterhuis
 */ 
public class PatrolState : IEnemyState {

	private Enemy enemy;
	private float patrolTimer; 
	private float patrolDuration = 10; 

	public void Execute ()
	{
		Patrol ();

		enemy.Move ();

		if (enemy.Target != null && enemy.inMeleeRange) {
			enemy.ChangeState (new MeleeState ());
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
		if (other.tag == "Edge") {
			enemy.ChangeDirection ();
		}
	}

	private void Patrol() {
		
		patrolTimer += Time.deltaTime; 

		if (patrolTimer >= patrolDuration) {
			enemy.ChangeState (new IdleState());
		}
	}
}
