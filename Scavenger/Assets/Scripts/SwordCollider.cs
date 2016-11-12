using UnityEngine;
using System.Collections;

/**
 * @author Nick Oosterhuis
 */ 
public class SwordCollider : MonoBehaviour {

	[SerializeField] private string targetTag;

	void onTriggerEnter2D (Collider2D other) {
		if (other.tag == targetTag) {
			GetComponent<Collider2D> ().enabled = false; 
		}
	}
}
