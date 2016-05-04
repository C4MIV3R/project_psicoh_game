using UnityEngine;
using System.Collections;

public class DestroyShotsByBoundary : MonoBehaviour
{
	void OnTriggerExit2D(Collider2D other)
	{
		GameObject.FindGameObjectsWithTag("Bullet");
		if (other.gameObject.tag == "Bullet")
		Destroy(other.gameObject);
	}
}
