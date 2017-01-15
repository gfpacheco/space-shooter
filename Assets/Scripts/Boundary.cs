using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary: MonoBehaviour {

	void Awake () {
		float height = 2 * Camera.main.orthographicSize;
		transform.localScale = new Vector3(
			height * Screen.width / Screen.height,
			0,
			height
		);
	}

	void OnTriggerExit (Collider other) {
		Destroy (other.gameObject);
	}

}
