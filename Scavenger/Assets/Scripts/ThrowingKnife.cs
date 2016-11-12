using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]

/**
 * this class initializes the throwing knife which is used by the player to do damage to enemies. 
 * it sets a speed for the throwing knife, a direction in which it moves and destroys the object when its not longer visible on the screen
 * 
 * @author Nick Oosterhuis 
 */ 
public class ThrowingKnife : MonoBehaviour {

	[SerializeField] private float speed; 
	private Rigidbody2D rb2d; 
	private Vector2 direction; 

	// init
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();  
	}

	void FixedUpdate() {
		rb2d.velocity = direction * speed; 
	}

	public void Initialize(Vector2 direction) {
		this.direction = direction; 
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
