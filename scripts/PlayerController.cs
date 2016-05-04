// TO DO:
// Respawn player after death
// animation for running and shooting front, up-diagonal, down-diagonal
// collision with enemies doesn't push enemies around (Necessary now that player dies on contact?)
// Game Over script/function

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
		// -------------- public variables ---------------
		public int numberOfLives = 11;
		public float maxSpeed = 15f;
		public float dashSpeed = 22f;
		public Transform groundCheck;
		public LayerMask whatIsGround;
		public float jumpForce = 700f;
		public GameObject basicShot;
		public Transform shotSpawn;
		public float fireRate;
		public bool facingRight = true;
		public bool isDead = false;

		// ---------------- private variables ---------------
		private Animator anim;
		private Rigidbody2D playerRigidbody;
		private AudioSource audioSource;
		private bool grounded = false;
		private float groundRadius = 1f;
		private bool IsRunning = false;
		private float nextFire;
		private PlayerController playerController;
		private GameObject enemy;
		private EnemyController enemyController;
		private Transform transform;

		// set up any variables prior to game actually beginning
		void Awake ()
		{
			playerRigidbody = GetComponent<Rigidbody2D>();
			playerController = GetComponent<PlayerController>();
			anim = GetComponent<Animator>();
			audioSource = GetComponent<AudioSource>();
			transform = GetComponent<Transform>();

		} // end Awake

		// using Start to respawn player on first frame of game
		void Start ()
		{
			Respawn();
		}

		void Update ()
		{
			if (grounded && Input.GetKeyDown(KeyCode.Space))
			{
				anim.SetBool("Ground", false);
				playerRigidbody.AddForce(new Vector2(0, jumpForce));
			}
			if (Input.GetKey(KeyCode.Z) && Time.time > nextFire)
			{
				Fire();
			}
			if (Input.GetKey(KeyCode.Z) != true)
			{
				anim.SetBool("IsFiring", false);
			}
		} // end Update

		// FixedUpdate called once per physics movement
		void FixedUpdate ()
		{
			grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); // checks for LAYER of 'Ground', not tag
			anim.SetBool("Ground", grounded);
			anim.SetFloat("vSpeed", playerRigidbody.velocity.y);
			// Get player's horizontal input via WASD or Arrow Keys
			float horz = Input.GetAxis("Horizontal");
			// float vert = Input.GetAxis("Vertical");
			// lookRight should be true when holding right arrow or 'D' key.
			// BUT RunningFrontShooting animation should only fire when both lookRight is true and fire is true
			// Otherwise use the basic running animation
			// bool runRight 	= horz > 0f;
			// bool runLeft 	= horz < 0f;
			// bool lookUp 		= vert > 0f;
			// bool lookDown 	= vert < 0f;

			bool running = horz != 0f;
			anim.SetBool("IsRunning", running);

			// set speed equal to movement speed
			anim.SetFloat("Speed", Mathf.Abs(horz));

			// Apply force to player model via playerRigidbody
			playerRigidbody.velocity = new Vector2(horz * maxSpeed, playerRigidbody.velocity.y);

			if (Input.GetKey(KeyCode.LeftShift) && horz != 0f)
			{
				Dash ();
			}
			if (Input.GetKey(KeyCode.LeftShift) != true)
			{
				maxSpeed = 15f;
				anim.SetBool("IsDashing", false);
			}

// ----------- Flip Block ----------------
			// check to see if player is moving right but facing left
			if ( horz > 0 && !facingRight)
				Flip ();
			// check to see if player is moving left but facing right
			else if ( horz < 0 && facingRight)
				Flip ();
		} // end FixedUpdate

		// function to flip player model
		void Flip ()
		{
			// Switch the direction the player is facing
			facingRight = !facingRight;
			// multiply the player's x local scale by -1
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
// ---------- end Flip Block --------------
		void Fire ()
		{
				anim.SetBool("IsFiring", true);
				nextFire = Time.time + fireRate;
				Instantiate (basicShot, shotSpawn.position, shotSpawn.rotation);
				audioSource.Play();
		} // end Fire

		// Dashing function - sets dash animation to true and sets maxSpeed to dashing speed
		void Dash ()
		{
			anim.SetBool("IsDashing", true);
			maxSpeed = dashSpeed;
		} // end Dash

		public void Death ()
		{
			// disable the player's ability to move
			playerController.enabled = false;
			// set isDead to true
			isDead = true;
			// trigger the IsDead animation
			anim.SetTrigger("IsDead");
			Respawn();
		} // end Death

		void Respawn ()
		{
			if (numberOfLives == 11)
			{
				numberOfLives = numberOfLives - 1;
			}
			else if (numberOfLives < 11 && numberOfLives > 0) {
				Vector3 spawnPoint = new Vector3(-37.54f, 12.03f, 0f);
				transform.position = spawnPoint;
				// re-enable player's ability to move
				playerController.enabled = true;
				// set isDead to false
				isDead = false;
				// reset IsDead animation - can I reverse the animation?
				anim.SetTrigger("IsDead");
				numberOfLives = numberOfLives - 1;
			}
			else
			{
				// need to create a function or script to go to for GAME OVER

				Debug.Log("GAME OVER");
			}
		}

		// Player dies on collision with enemy
		void OnCollisionEnter2D(Collision2D coll)
		{
			// if player runs into a live Enemy...
			if (coll.gameObject.tag == "Enemy")
			{
				// kill player
				Death();
			}
		} //  end OnCollisionEnter2D

		void GameOver()
		{
			
		}
} // end PlayerController
