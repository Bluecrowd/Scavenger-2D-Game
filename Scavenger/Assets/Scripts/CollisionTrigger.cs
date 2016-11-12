using UnityEngine;
using System.Collections;

/**
 * This class will make it able to let the player walk through platforms in order to choose a path they want to go through. 
 * When a player is in the middel of a certain platform and jumps up the collider is activated again
 * 
 * @author Nick Oosterhuis
 */ 
public class CollisionTrigger : MonoBehaviour {

	private BoxCollider2D playerCollider; 

	[SerializeField] private BoxCollider2D platformCollider; 
	[SerializeField] private BoxCollider2D platformTrigger;

	// init
	void Start () {
		playerCollider = GameObject.Find ("Player").GetComponent<BoxCollider2D> (); 
		Physics2D.IgnoreCollision (platformCollider, platformTrigger, true);
	}

	/**
	 * Ignore the collission if the player is entering the trigger
	 */ 
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name == "Player") {
			Physics2D.IgnoreCollision (platformCollider, playerCollider, true);
		}
	}
	/*
	 * Dont ignore the collission when the player has exited the trigger
	 */
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.name == "Player") {
			Physics2D.IgnoreCollision (platformCollider, playerCollider, false);
		}
	}
}
