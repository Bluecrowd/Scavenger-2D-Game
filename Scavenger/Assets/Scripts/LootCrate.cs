using UnityEngine;
using System.Collections;

/**
 * This class is the script for the lootCrates in the game which can be seen as coins to increase the score.
 * 
 * @author Nick Oosterhuis
 */ 
public class LootCrate : MonoBehaviour {
	private GameMaster gm;

	// init
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
	}
	//pickup the lootcrate and increase the game score
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			Destroy (gameObject);
			gm.currentScore += 1; 
		}
	}
}
