// TO DO:
// Enemy AI - on player entering trigger area - begin tracking and firing at player
// Patrol routes
// On being killed - Destroy enemy gameObject

using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float maxSpeed;
	public float jumpForce;
	public LayerMask whatIsPlayer;
	public float patrolSpeed = 8f;
	public float chaseSpeed = 12f;
	public float patrolWaitTime = 2f;
	public float chaseWaitTime = 6f;
	public Transform[] patrolWaypoints;
	public bool isDead = false;

	private Animator anim;
	private GameObject enemy;
	private GameObject enemyController;
	private GameObject player;
	private PlayerController playerController;
	private PolygonCollider2D polygonCollider2D;
	private Rigidbody2D rigidbody2D;
	private BoxCollider2D boxCollider2D;
	private AudioSource audioSource;
	private SpriteRenderer spriteRenderer;
	private Color spriteRendColor;

	void Awake ()
	{
		GameObject player = GameObject.Find("Player");
		playerController = player.GetComponent<PlayerController>();
	 	polygonCollider2D = GetComponent<PolygonCollider2D>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		boxCollider2D = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRendColor = spriteRenderer.color;
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

		}
	} // end OnTriggerEnter2D

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{

		}
	} // end OnTriggerStay2D

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{

		}
	} // end OnTriggerExit2D

	void Death ()
	{
		// StartCoroutine(WaitTime(5));
		anim.SetTrigger("IsDead");
		audioSource.Play();
		polygonCollider2D.enabled = false;
		rigidbody2D.isKinematic = true;
		boxCollider2D.enabled = false;
		spriteRendColor = Color.Lerp(Color.white, Color.clear, 5);

		this.enabled = false;
		// Destroy(gameObject);
	} // end Death

	// IEnumerator WaitTime (float time)
	// {
	// 	Debug.Log("Begin wait for 5 seconds.");
	// 	yield return new WaitForSeconds(time);
	// 	Debug.Log("Done waiting.");
	// }
} // end EnemyController
