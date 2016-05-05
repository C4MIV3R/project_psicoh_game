// Throughout all scripts - ~500 lines of code so far (May 5th, 2016)

// TO DO:
// Respawn player after death - DONE
// animations for:
// runningFront - DONE
// running and shooting up and down
// collision with enemies doesn't push enemies around (Necessary now that player dies on contact?)
// Game Over script/function

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
		// -------------- public variables ---------------

		public int numberOfLives = 5;
		public float speed = 15f;
		public float dashSpeed = 22f;
		public Transform groundCheck;
		public LayerMask whatIsGround;
		public float jumpForce = 700f;
		public GameObject basicShot;
		public Transform shotSpawn;
		public float fireRate;
		public bool facingRight = true;
		public bool isDead = false;
		public float timeToWait = 5f;

		// ---------------- private variables ---------------
		private Animator anim;
		private Rigidbody2D playerRigidbody;
		private AudioSource audioSource;
		private bool grounded = false;
		private float groundRadius = 1f;
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
			if (!grounded && Input.GetKey(KeyCode.Z) && Time.time > nextFire)
			{
				Fire();
			}
		} // end Update

		// FixedUpdate called once per physics movement
		void FixedUpdate ()
		{
			grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); // checks for LAYER of 'Ground', not tag
			anim.SetBool("Ground", grounded);
			anim.SetFloat("vSpeed", playerRigidbody.velocity.y);
			// Get player's horizontal input via WASD or Arrow Keys
			float horz 			= Input.GetAxis("Horizontal");
			float vert 			= Input.GetAxis("Vertical");
			bool lookUp 		= vert > 0f;
			bool lookDown 	= vert < 0f;
			bool running 		= horz != 0f;

			// set speed equal to movement speed
			anim.SetFloat("Speed", Mathf.Abs(horz));
			// set vertLook equal to looking up or down float
			anim.SetFloat("vertLook", Mathf.Abs(vert));
			// Set IsRunning based on if horz != 0
			anim.SetBool("IsRunning", running);
			anim.SetBool("lookUp",lookUp);
			anim.SetBool("lookDown", lookDown);

			// Apply force to player model via playerRigidbody
			playerRigidbody.velocity = new Vector2(horz * speed, playerRigidbody.velocity.y);

			if (Input.GetKey(KeyCode.LeftShift) && horz != 0f)
			{
				Dash ();
			}
			if (Input.GetKey(KeyCode.LeftShift) != true)
			{
				speed = 15f;
				anim.SetBool("IsDashing", false);
			}
			if (lookUp && running) {
				anim.SetBool("RunningLookingUp", true);
			}
			if (lookDown && running) {
				anim.SetBool("RunningLookingDown", true);
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

		// Dashing function - sets dash animation to true and sets speed to dashing speed
		void Dash ()
		{
			anim.SetBool("IsDashing", true);
			speed = dashSpeed;
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
			if (numberOfLives <= 10 && numberOfLives > 0) {
				Vector3 spawnPoint = new Vector3(-37.54f, 12.03f, 0f);
				transform.position = spawnPoint;
				// re-enable player's ability to move
				playerController.enabled = true;
				// set isDead to false
				isDead = false;
				// trigger IsDead
				anim.SetTrigger("IsDead");
				// subtract 1 from the numberOfLives
				numberOfLives = numberOfLives - 1;
			}
			else
			{
				// TO BE IMPLEMENTED
				// GameOver();
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
			// if player is hit by EnemyBullet...
			if (coll.gameObject.tag == "EnemyBullet")
			{
				// kill player
				Death();
			}
		} //  end OnCollisionEnter2D

		// TO BE IMPLEMENTED
		// void GameOver()
		// {
		//
		// }
} // end PlayerController
