using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void DeadEventHandler(); 

/**
 * This class consists of all methods in order to move the player, let it die, take damage, switch between animations, attack enemies, 
 * respawn, set controls and set an immortality rate. 
 * 
 * @author Nick Oosterhuis 
 */ 
public class Player : Character {

	private static Player instance;  
	private Vector2 startPos; 
	public event DeadEventHandler Dead; 

	[SerializeField] private float yMinRespawning; 

	private bool immortal = false;
	[SerializeField]private float immortalTime; 
	private SpriteRenderer sprr;  

	[SerializeField] private Transform knifePosition; 
	 
	[SerializeField] private float jumpForce; 
	[SerializeField] private GameObject knifePrefab;
	 
	[SerializeField] private LayerMask whatIsGround; 
	[SerializeField] private bool airControl;
	[SerializeField] private float groundRadius;
	[SerializeField] private Transform[] groundPoints;  

	public Rigidbody2D Rb2d { get; set; }
	public bool Slide { get; set; }
	public bool Jump { get; set; }
	public bool OnGround { get; set; }

	// init
	public override void Start () {
		base.Start ();
		startPos = transform.position;  
		sprr = GetComponent<SpriteRenderer> (); 
		Rb2d = GetComponent<Rigidbody2D> (); 

	}

	void Update() {
		if (!TakingDamage && !isDead) {
			if (transform.position.y <= yMinRespawning) {
				Death ();
			}
		}
		HandleInput ();
	}

	void FixedUpdate () {
		if (!TakingDamage && !isDead) {
			float move = Input.GetAxis ("Horizontal");
			OnGround = IsGrounded (); 

			HandleMovement (move);
			Flip (move);
			HandleLayers (); 
		}
	}

	public void onDead() {
		if (Dead != null) {
			Dead ();
		}
	}

	public static Player Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<Player> (); 
			}
			return instance;
		}
	}
	/**
	 * handle the movement animations while jumping
	 */  
	private void HandleMovement(float move) {
		if (Rb2d.velocity.y < 0) {
			Anim.SetBool ("Land", true); 
		}
		if (!Attack && !Slide || (OnGround || airControl)) {
			Rb2d.velocity = new Vector2 (move * movementSpeed, Rb2d.velocity.y);
		}
		if (Jump && Rb2d.velocity.y == 0) {
			Rb2d.AddForce (new Vector2(0, jumpForce));
		}

		Anim.SetFloat ("Speed", Mathf.Abs (move));
	}

	/**
	 * Controls for moving the player
	 */ 
	private void HandleInput() {
		if (Input.GetKeyDown (KeyCode.Mouse0) || Input.GetKeyDown (KeyCode.LeftControl)) {
			Anim.SetTrigger ("Attack");
		}
		if (Input.GetKeyDown (KeyCode.Mouse1) || Input.GetKeyDown (KeyCode.LeftAlt)) {
			Anim.SetTrigger ("Throw");
		}
		if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)) {
			Anim.SetTrigger ("Slide");
		}
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) {
			Anim.SetTrigger ("Jump");
		}
	}

	/**
	 * flip the player in the correct direction
	 */ 
	private void Flip(float move) {
		if (move > 0 && !facingRight || move < 0 && facingRight) {
			ChangeDirection ();  
		}
	}

	/**
	 * boolean to check if the player is on the ground at 3 certain points on the player. 
	 */ 
	private bool IsGrounded() {
		if (Rb2d.velocity.y <= 0) {
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);

				for (int i = 0; i < colliders.Length; i++) {
					if (colliders [i].gameObject != gameObject) {
						return true; 
					}
				}
			}
		} return false; 
	}
	/**
	 * set animation layer weight
	 */  
	private void HandleLayers() {
		if (!OnGround) {
			Anim.SetLayerWeight (1, 1);
		} else {
			Anim.SetLayerWeight (1, 0);
		}
	}

	/**
	 * throw a knife in a certain direction
	 */ 
	public void ThrowKnife(int value) {
		if (!OnGround && value == 1 || OnGround && value == 0) {
			if (facingRight) {
				GameObject tmp = (GameObject)Instantiate (knifePrefab, knifePosition.position, Quaternion.Euler (new Vector3 (0, 0, -90)));
				tmp.GetComponent<ThrowingKnife> ().Initialize (Vector2.right);
			} else if (!facingRight) {
				GameObject tmp = (GameObject)Instantiate (knifePrefab, knifePosition.position, Quaternion.Euler (new Vector3 (0, 0, 90)));
				tmp.GetComponent<ThrowingKnife> ().Initialize (Vector2.left);
			}
		}
	}

	/**
	 * set the player immortal for a brief moment of time when it is being hit by an enemy
	 */ 
	private IEnumerator IndicateImmortal() {
		while (immortal) {
			sprr.enabled = false; 
			yield return new WaitForSeconds (.1f); 
			sprr.enabled = true; 
			yield return new WaitForSeconds (.1f); 
		}
	}

	/**
	 * take damage when being hit, lower health and play the damage animation 
	 */ 
	public override IEnumerator TakeDamage ()
	{
		if (!immortal) {
			currentHealth -= 10; 
			healthBar.fillAmount = (float)currentHealth / maxHealth; 
			if (!isDead) {
				Anim.SetTrigger ("Damage");
				immortal = true; 
				StartCoroutine (IndicateImmortal ());
				yield return new WaitForSeconds (immortalTime); 
				immortal = false;
			} else {
				Anim.SetLayerWeight (1,0);
				Anim.SetTrigger ("Die");
			}
		}
	}
	/**
	 * boolean to check if the player is dead
	 */ 
	public override bool isDead {
		get {
			if (currentHealth <= 0) {
				onDead ();
			}
			return currentHealth <= 0; 
		}
	}
	/**
	 * let the player die, reset the velocity. go back to idle state. reset the level
	 */ 
	public override void Death ()
	{
		Rb2d.velocity = Vector2.zero; 
		Anim.SetTrigger ("Idle");
		currentHealth = maxHealth; 
		transform.position = startPos;

		//reset the level when the player is dead.  
		Application.LoadLevel (Application.loadedLevel);
	}
}
