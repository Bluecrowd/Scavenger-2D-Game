using UnityEngine;
using System.Collections;

public interface IEnemyState {
	void Execute ();
	void Enter (Enemy enemy); 
	void Exit ();
	void OntriggerEnter (Collider2D other); 
}
