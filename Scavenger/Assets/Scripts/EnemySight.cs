using UnityEngine;
using System.Collections;

/**
 * This class is for the field of view of the enemy. If the player is in that field of view the enemy follows him and starts to attack. 
 * 
 * @author Nick Oosterhuis
 */
public class EnemySight : MonoBehaviour {

	[SerializeField] private Enemy enemy; 

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			enemy.Target = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			enemy.Target = null; 
		}
	}
}
