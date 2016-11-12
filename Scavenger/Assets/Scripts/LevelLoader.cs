using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	private GameMaster gm; 

	private bool playerInZone; 
	[SerializeField] private string levelToLoad; 

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		playerInZone = false; 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && playerInZone == true && gm.currentScore == gm.maxScore) {
			Application.LoadLevel (levelToLoad);
		} else if (Input.GetKeyDown (KeyCode.Space) && playerInZone == true && gm.currentScore != gm.maxScore){
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Player") {
			playerInZone = true; 
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.name == "Player") {
			playerInZone = false; 
		}
	}
}
