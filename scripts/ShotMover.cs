// TO DO:
// Shooting bullets left - DONE
// On bullet collision wth enemy - destroy bullet - DONE
// On bullet collision with enemy - kill enemy - DONE

using UnityEngine;
using System.Collections;

public class ShotMover : MonoBehaviour
{
	public float speed;

	private Rigidbody2D shotRigidbody;
	private GameObject facingRight;

	void Start ()
	{
		shotRigidbody = GetComponent<Rigidbody2D>();
		// Finds gameObject of Player
		GameObject player = GameObject.Find("Player");
		// Finds PlayerController script within Player object
    PlayerController playerController = player.GetComponent<PlayerController>();

		// if facingRight within playerController is true - fire projectile to the right
		if (playerController.facingRight)
		{
			shotRigidbody.velocity = transform.right * speed;
		}
		// if facingRight within playerController is false - fire projectile to the left
		if (!playerController.facingRight)
		{
			shotRigidbody.velocity = -transform.right * speed;
		}
	} // end Start

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Enemy")
		{
			// Debug.Log("Enemy has been hit with a Bullet");
			Destroy(gameObject);
			// Debug.Log(coll.gameObject);

		}
		if (coll.gameObject.tag == "Ground")
		{
			// Debug.Log("Ground has been hit with a Bullet");
			Destroy(gameObject);
		}
		if (coll.gameObject.tag == "Edges")
		{
			// Debug.Log("Edge has been hit with a Bullet");
			Destroy(gameObject);
		}
	} //end OnCollisionEnter2D
} // end class ShotMover
