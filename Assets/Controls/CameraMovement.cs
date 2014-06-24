using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float moveSpeed = 10.0f;
	public float rotateSpeed = 90.0f;
	public GameObject cameraXPivot;

	private Vector3 movement = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		movement.x = Input.GetAxis("Horizontal");
		movement.z = Input.GetAxis("Vertical");

		if (movement.magnitude > 1)
			movement = movement.normalized;

		transform.Translate(movement * Time.deltaTime * moveSpeed);

		if (Input.GetKey("e")) {
			this.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
		} else if (Input.GetKey("q")) {
			this.transform.Rotate(Vector3.up * -rotateSpeed * Time.deltaTime, Space.World);
		}
	}
}
