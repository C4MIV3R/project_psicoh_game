// TO DO:
// Enemy AI - on player entering trigger area - begin tracking and firing at player - MVP done
// Patrol routes
// On being killed - Destroy enemy gameObject

using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject enemyBasicShot;
	public Transform enemyShotSpawn;
	public float maxSpeed;
	public float jumpForce;
	public float patrolSpeed = 8f;
	public float chaseSpeed = 12f;
	public float patrolWaitTime = 2f;
	public float chaseWaitTime = 6f;
	public Transform[] patrolWaypoints;
	public bool isDead = false;
	public float fireRate = 0.5f;
	public float angle;

	private Animator anim;
	private GameObject enemy;
	private GameObject enemyController;
	private GameObject player;
	private PlayerController playerController;
	private PolygonCollider2D polygonCollider2D;
	private Rigidbody2D rigidbody2D;
	private BoxCollider2D boxCollider2D;
	private AudioSource audioSource;
	private Transform playerTransform;
	private Vector3 playerPosition;
	private Vector3 playerDir;
	private float nextFire;


	void Awake ()
	{
		GameObject player = GameObject.Find("Player");
		playerController = player.GetComponent<PlayerController>();
	 	polygonCollider2D = GetComponent<PolygonCollider2D>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		boxCollider2D = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		playerTransform = player.transform;
		playerPosition = playerTransform.position;
	} // end Awake

	// Update is called once per frame
	void Update ()
	{
		if (playerController.isDead == true)
		{
			this.enabled = false;
		}
		else
		{
			this.enabled = true;
		}
		playerPosition = playerTransform.position;

	} // end Update

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Bullet")
		{
			Death();
		}
	} // end OnCollisionEnter2D

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			TurnToFace();
		}
	} // end OnTriggerEnter2D

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			TurnToFace();
			if (Time.time > nextFire)
			{
				Fire();
			}
		}
	} // end OnTriggerStay2D

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			anim.SetBool("IsFiring", false);
		}
	}

	void TurnToFace ()
	{
		playerDir = playerPosition - transform.position;
		playerDir.y = 0.0f;
		angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
		// if angle = 180 - enemy facing left. if angle = 0 - enemy facing right.
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
	}

	void Death ()
	{
		// StartCoroutine(WaitTime(5));
		anim.SetTrigger("IsDead");
		audioSource.Play();
		polygonCollider2D.enabled = false;
		rigidbody2D.isKinematic = true;
		boxCollider2D.enabled = false;

		this.enabled = false;
		// Destroy(gameObject);
	} // end Death

	void Fire ()
	{
		anim.SetBool("IsFiring", true);
		nextFire = Time.time + fireRate;
		Instantiate (enemyBasicShot, enemyShotSpawn.position, enemyShotSpawn.rotation);
		audioSource.Play();
	}

	// IEnumerator WaitTime (float time)
	// {
	// 	Debug.Log("Begin wait for 5 seconds.");
	// 	yield return new WaitForSeconds(time);
	// 	Debug.Log("Done waiting.");
	// }
} // end EnemyController
