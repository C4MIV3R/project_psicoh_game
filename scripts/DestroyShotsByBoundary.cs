using UnityEngine;
using System.Collections;

public class DestroyShotsByBoundary : MonoBehaviour
{
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Bullet")
		Destroy(other.gameObject);
	}
}
