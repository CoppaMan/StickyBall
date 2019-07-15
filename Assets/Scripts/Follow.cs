using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	private List<GameObject> obstructions;
	public float distance;
	private Stats katamari;
	private float originalSize;
	public bool manual = true;
	public float orbitAngle = 0;
	private float tiltAngle = 45;
	public float minTilt = 0;
	public float maxTilt = 90;
	public float tiltSpeed = 2;
	private float deadZone = .001f;

	// Use this for initialization
	void Start () {
		//obstructions = new List<GameObject>();
		katamari = GameObject.Find("Katamari").GetComponent<Stats>();
		originalSize = katamari.length;
	}
	
	// Update is called once per frame
	void Update () {
		//Tilt from horizontal to up
		if (Mathf.Abs(Input.GetAxisRaw("VerticalR")) > deadZone) {
			tiltAngle += Input.GetAxisRaw("VerticalR")*tiltSpeed;
			tiltAngle = tiltAngle < minTilt ? minTilt : (tiltAngle > maxTilt ? maxTilt : tiltAngle);
			//Debug.Log(tiltAngle);
		}
		Quaternion tilt = Quaternion.AngleAxis(tiltAngle, Vector3.right);

		//Orbit around the object
		if (manual) {
			if (Mathf.Abs(Input.GetAxisRaw("HorizontalR")) > deadZone) {
				orbitAngle += Input.GetAxisRaw("HorizontalR")*tiltSpeed;
			}
		} else {
			//Vector3 velocity = VectorMath.Flatten(GameObject.Find("Katamari").GetComponent<Rigidbody>().velocity);
			//float currentOrbitAngle = Vector3.SignedAngle(Vector3.forward, velocity, Vector3.up);
		}
		Quaternion orbit = Quaternion.AngleAxis(orbitAngle, Vector3.up);

		Vector3 relPos = orbit*tilt*Vector3.back*distance*(katamari.length/originalSize);
		gameObject.transform.position = relPos + katamari.transform.position;
		gameObject.transform.rotation = orbit*tilt;

		//OpacityUpdate();
	}

	private void OpacityUpdate() { //Need fade rendering mode, not changable for blender materials
		foreach (GameObject g in obstructions) {
			Color current = g.GetComponent<Renderer>().material.color;
			current.a = 0.5f;
			Debug.Log(g);
			g.GetComponent<Renderer>().material.color = current;
		}
	}

/*
	private void OnTriggerEnter(Collider other)
    {
		if(other.GetType() == typeof(MeshCollider)) {
			obstructions.Add(other.gameObject);
		}
    }

	private void OnTriggerExit(Collider other)
    {
		if(other.GetType() == typeof(MeshCollider)) {
			obstructions.Remove(other.gameObject);
			Color current = other.gameObject.GetComponent<Renderer>().material.color;
			current.a = 1f;
			other.gameObject.GetComponent<Renderer>().material.color = current;
		}
    }
		*/
}
