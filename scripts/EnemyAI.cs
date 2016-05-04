using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public LayerMask whatIsPlayer;
	public float patrolSpeed = 8f;
	public float chaseSpeed = 12f;
	public float patrolWaitTime = 2f;
	public float chaseWaitTime = 6f;
	public Transform[] patrolWaypoints;

	private Animator anim;

	void Awake ()
	{

	}



}
