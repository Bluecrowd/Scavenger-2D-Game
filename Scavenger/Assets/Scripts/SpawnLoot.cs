using UnityEngine;
using System.Collections;
/**
 * This class spawns lootcrates at certain gme object points and coints the amount of loot to view the max score in the game master.
 * 
 * @author Nick Oosterhuis
 */ 
public class SpawnLoot : MonoBehaviour {

	private GameMaster gm;
	[SerializeField] private Transform[] lootSpawns;
	[SerializeField] private GameObject lootCrate;  
	public int maxLoot; 


	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		Spawn ();
	}
		
	void Spawn() {
		maxLoot = lootSpawns.Length;
		gm.maxScore = maxLoot; 

		for (int i = 0; i < lootSpawns.Length; i++) {
			Instantiate (lootCrate, lootSpawns [i].position, Quaternion.identity);
		}
	}
}
	