using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * This class makes sure the score is being displayed in the level. UI component.
 * 
 * @author Nick Oosterhuis 
 */ 
public class GameMaster : MonoBehaviour {

	public int currentScore;
	public int maxScore; 
	[SerializeField] private Text scoreText; 
	
	// update the score of the score text
	void Update () {
		scoreText.text = ("Score: " + currentScore + "/" + maxScore); 
	}
}
