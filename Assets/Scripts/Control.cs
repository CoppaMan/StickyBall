using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

	Rigidbody rb;
	Stats stats;
	Vector3 target;
	float torque = 5;
	public bool movable;
	private Follow camera;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		stats = GetComponent<Stats>();
		camera = GameObject.Find("GameView").GetComponent<Follow>();
		movable = true;
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion orbit = Quaternion.AngleAxis(camera.orbitAngle, Vector3.up); //View angle
		target = orbit*Vector3.Normalize(Input.GetAxisRaw("Horizontal")*Vector3.right + Input.GetAxisRaw("Vertical")*Vector3.forward);
	}

	void FixedUpdate() {
		if (movable) {
			rb.AddForce(target*3*stats.length, ForceMode.Force);
			rb.AddTorque(Vector3.Cross(target, Vector3.down)*7*stats.length, ForceMode.Force);
		}
	}
}
