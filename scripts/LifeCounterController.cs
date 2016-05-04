// TO DO:
// Number of lives working - DONE

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeCounterController : MonoBehaviour {

	public int numberOfLives;

	private Text text;
	private GameObject player;
	private PlayerController playerController;

	void Awake () {
		text = GetComponent<Text>();
		player = GameObject.Find("Player");
		playerController = player.GetComponent<PlayerController>();
	} // end Awake

	// Update is called once per frame
	void Update () {
		numberOfLives = playerController.numberOfLives; // update numberOfLives every frame
		text.text = "x" + numberOfLives;
	}
} // end Update
