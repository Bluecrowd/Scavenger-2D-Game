using UnityEngine;
using System.Collections;

/**
 * This class maintains the behavior of the enemies in the game. It has various methods to switch states,
 * do damage, take damage, despawn dead enemies and look at the player. 
 * 
 * @author Nick Oosterhuis
 */
public class Enemy : Character {

	private IEnemyState currentState;
	[SerializeField] private float meleeRange; 
	public GameObject Target { set; get; } 

	// init
	public override void Start () {
		base.Start ();
		Player.Instance.Dead += new DeadEventHandler (RemoveTarget); 
		ChangeState (new IdleState());
	}
	
	// execute the current state the enemy is in and initialize looking at target
	void Update () {

		if (!isDead) {
			if (!TakingDamage) {
				currentState.Execute ();
			}
			LookAtTarget ();
		} 
	}

	/**
	 * if target isn't in reach go back to patrolling state
	 */ 
	public void RemoveTarget() {
		Target = null;
		ChangeState (new PatrolState());
	}
	/**
	 * if the target is in site follow the target and switch directions 
	 * when te target moves in another direction
	 */ 
	private void LookAtTarget() {
		if (Target != null) {
			float xDir = Target.transform.position.x - transform.position.x; 

			if (xDir < 0 && facingRight || xDir > 0 && !facingRight) {
				ChangeDirection ();
			}
		}
	}

	/**
	 * method to switch between the different enemy states
	 */ 
	public void ChangeState(IEnemyState newState) {
		if (currentState != null) {
			currentState.Exit ();
		}
		currentState = newState; 
		currentState.Enter (this);
	}

	/**
	 * method to let the enemy move with a certain speed. 
	 */ 
	public void Move() {
		if (!Attack) {
				Anim.SetFloat ("Speed", 1);
				transform.Translate (GetDirection () * (movementSpeed * Time.deltaTime),Space.World);
		}
	}
	/**
	 * get the direction in which the enemy is facing
	 */ 
	public Vector2 GetDirection() {

		return facingRight ? Vector2.right : Vector2.left; 
	}

	/**
	 * ignore the collission between multiple enemies
	 */ 
	public override void OnTriggerEnter2D(Collider2D other) {
		base.OnTriggerEnter2D (other);
		currentState.OntriggerEnter (other);

		if (other.tag == "Enemy") {
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), other, true);
		}
	}

	/**
	 * do 10 damage to the health of an enemy when it is being attacked and play the damage animation or the die animation
	 */
	public override IEnumerator TakeDamage () {
		currentHealth -= 10; 

		if (!isDead) {
			Anim.SetTrigger ("Damage");
		} else {
			Anim.SetTrigger ("Die");
			 
		} yield return null;
	}
	/**
	 * bool to check if a enemy is dead
	 */ 
	public override bool isDead {
		get {
			return currentHealth <= 0; 
		}
	}
	/**
	 * destroy the enemy when it's dead 
	 */ 
	public override void Death ()
	{
		Destroy (gameObject); 
	}
	/*
	 * the melee range of the enemy. this methods specificates when the enemy can start attacking when the target is at a certain distance
	 */ 
	public bool inMeleeRange {
		get {
			if (Target != null) {
				return Vector2.Distance (transform.position, Target.transform.position) <= meleeRange;
			} else {
				return false; 
			}
		}
	}
}
