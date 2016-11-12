using UnityEngine;
using System.Collections;

/*
 * This class is the camera follow class it follows the player around. 
 * 
 * @param float xMax the x-axis max of the level  
 * @param float yMax the y-axis max of the level 
 * @param float xMin the x-axis min of the level 
 * @param float yMin the y-axis min of the level 
 * 
 * @author Nick Oosterhuis  
 */
public class CameraFollow : MonoBehaviour {

	[SerializeField] private float xMax; 
	[SerializeField] private float yMax; 
	[SerializeField] private float xMin; 
	[SerializeField] private float yMin; 

	private Transform target; 

	void Start () {
		target = GameObject.Find ("Player").transform;
	}

	void LateUpdate () {
		transform.position = new Vector3 (Mathf.Clamp (target.position.x, xMin, xMax), Mathf.Clamp (target.position.y, yMin, yMax), transform.position.z);  
	}
}
