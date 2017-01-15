using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private GameController gameController;

	void Start () {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");

		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object");
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Boundary") {
			return;
		}

		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		} else {
			gameController.IncrementScore (scoreValue);
		}

		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}

}
