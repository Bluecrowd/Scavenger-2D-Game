using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.UI; 

/**
 * 
 * This class contains various of shared abstract methods which are both used by the enemy and player scripts. 
 * 
 * @author Nick Oosterhuis 
 */ 
public abstract class Character : MonoBehaviour {

	protected bool facingRight;
	public int currentHealth; 
	[SerializeField] protected int maxHealth; 
	[SerializeField] protected Image healthBar; 
	[SerializeField] protected float movementSpeed;

	[SerializeField] private EdgeCollider2D DamageCollider; 
	[SerializeField] private List<string> damageSources; 

	public abstract bool isDead{ get; } 
	public bool TakingDamage { get; set; }
	public bool Attack { get; set; }
	public Animator Anim { get; private set; }

	// init
	public virtual void Start () {
		facingRight = true;
		Anim = GetComponent<Animator> ();
		currentHealth = maxHealth; 
	}

	/**
	 * Abstract method to take damage as a player or enemy which has their own implementation per entity
	 */
	public abstract IEnumerator TakeDamage ();

	/**
	 * abstract method to set a player or enemy dead.
	 */ 
	public abstract void Death (); 

	/**
	 * If the player and enemies are moving from left to right they will flip around
	 */ 
	public void ChangeDirection() {
		facingRight = !facingRight; 
		transform.localScale = new Vector3 (transform.localScale.x * -1, 1, 1); 
	}
	/*
	 * Method in order to do a melee attack as a player or enemy by enabeling and disabeling the damage collider
	 * Sometimes this method bugs out and stays on after attacking once. 
	 */ 
	public void MeleeAttack() {
		if (!Attack) {
			DamageCollider.enabled = !DamageCollider.enabled;
		} else if (Attack) {
			DamageCollider.enabled = DamageCollider.enabled; 
		}
	}
	/**
	 * Start taking damage when a knife or swordcollider hits the character
	 */ 
	public virtual void OnTriggerEnter2D(Collider2D other) {
		if (damageSources.Contains(other.tag)) {
			StartCoroutine (TakeDamage ());
		}
	}
}
