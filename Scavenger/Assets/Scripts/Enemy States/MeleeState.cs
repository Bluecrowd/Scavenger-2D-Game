using UnityEngine;
using System.Collections;

/**
 * Describes the meleestate of the enemy. it has a cooldown before it can attack again.
 * @author Nick Oosterhuis 
 */ 
public class MeleeState : IEnemyState {

	private Enemy enemy; 
	private float meleeTimer; 
	private float meleeCoolDown = 3;
	private bool canHit = true; 

	public void Execute ()
	{
		Melee ();
		if (enemy.inMeleeRange) {
			enemy.ChangeState (new MeleeState ());
		}
		if (enemy.Target != null) {
			enemy.Move (); 
		} else {
			enemy.ChangeState (new IdleState());
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

	public void Melee() {
		meleeTimer += Time.deltaTime; 

		if (meleeTimer >= meleeCoolDown) {
			canHit = true; 
			meleeTimer = 0; 
		}
		if (canHit) {
			canHit = false; 
			enemy.Anim.SetTrigger ("Attack");
		}
	}
}
