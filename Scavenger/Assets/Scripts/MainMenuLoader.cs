using UnityEngine;
using System.Collections;

public class MainMenuLoader : MonoBehaviour {

	private bool playerInZone; 
	private string levelToLoad = "MainMenu"; 

	// Use this for initialization
	void Start () {
		playerInZone = false; 
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && playerInZone == true) {
			Application.LoadLevel (levelToLoad);
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
