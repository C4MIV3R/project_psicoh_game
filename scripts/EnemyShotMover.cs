using UnityEngine;
using System.Collections;

public class EnemyShotMover : MonoBehaviour
{
	public float speed;

	private Rigidbody2D shotRigidbody;
	private GameObject enemy;
	private EnemyController enemyController;

	void Start ()
	{
		shotRigidbody = GetComponent<Rigidbody2D>();
		// Finds gameObject of enemy
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		// Finds EnemyController script within Enemy object
    enemyController = enemy.GetComponent<EnemyController>();

		// fire in a straight line from where enemy is facing
		shotRigidbody.velocity = transform.right * speed;
	} // end Start

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground")
		{
			// Debug.Log("Ground has been hit with a ENEMYBullet");
			Destroy(gameObject);
		}
		if (coll.gameObject.tag == "Edges")
		{
			// Debug.Log("Edge has been hit with a ENEMYBullet");
			Destroy(gameObject);
		}
	} //end OnCollisionEnter2D
} // end class ShotMover
