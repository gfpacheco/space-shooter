using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Boundary boundary;
	public GameObject hazzard;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public int hazzardCount;
	public float startWait;
	public float spawnWait;
	public float waveWait;

	private float spawnMinX, spawnMaxX, spawnZ;
	private bool gameOver;
	private bool restart;
	private int score;

	void Start () {
		float halfBoundaryWidth = boundary.transform.localScale.x / 2;
		spawnMinX = -halfBoundaryWidth;
		spawnMaxX = halfBoundaryWidth;
		spawnZ = boundary.transform.position.z + (boundary.transform.localScale.z / 2) + 1;

		gameOverText.text = "";
		restartText.text = "";
		gameOver = false;
		restart = false;
		score = 0;

		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update () {
		if (restart && Input.GetButton ("Fire1")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);

		while (true) {
			for (int i = 0; i < hazzardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (spawnMinX, spawnMaxX), 0, spawnZ);
				Instantiate (hazzard, spawnPosition, Quaternion.identity);
				yield return new WaitForSeconds (spawnWait);
			}

			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restartText.text = "Click to restart";
				restart = true;
				break;
			}
		}
	}

	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}

	public void IncrementScore (int amount) {
		score += amount;
		UpdateScore ();
	}

	public void GameOver () {
		gameOverText.text = "Game Over";
		gameOver = true;
	}

}
