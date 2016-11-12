using UnityEngine;
using System.Collections;
/**
 * This class is to makes sure the collision between the enemies and the player are being ignorred 
 * when a player moves on top of the enemy or the other way arround. 
 * 
 * @author Nick Oosterhuis
 */
public class IgnoreCollision : MonoBehaviour {

	[SerializeField] private Collider2D other; 

	void Awake () {
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), other, true);
	}
}
