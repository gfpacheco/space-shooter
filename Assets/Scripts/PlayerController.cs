using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Boundary boundary;
	public GameObject shot;
	public Transform showSpawn;
	public float speed;
	public float tilt;
	public float fireRate;

	private Rigidbody rb;
	private float xMin, xMax;
	private float nextFire;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		Collider cl = GetComponent<Collider> ();

		float halfBoundaryWidth = boundary.transform.localScale.x / 2;
		float halfWidth = cl.bounds.size.x / 2;
		xMin = -halfBoundaryWidth + halfWidth;
		xMax = halfBoundaryWidth - halfWidth;
		nextFire = Time.time + fireRate;
	}

	void Update() {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			Instantiate (shot, showSpawn.position, showSpawn.rotation);
			nextFire = Time.time + fireRate;
		}
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 movement = new Vector3 (2 * Input.acceleration.x + moveHorizontal, 0, 0);

		rb.velocity = movement * speed;
		rb.rotation = Quaternion.Euler (0, 0, rb.velocity.x * -tilt);
		rb.position = new Vector3 (Mathf.Clamp (rb.position.x, xMin, xMax), 0, 0);
	}

}
