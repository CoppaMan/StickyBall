using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

	public Vector3 center;
	public GameObject source;
	public float maxHeightDiff = 5;
	public float radius = 5;
	public int copies = 20;

	// Use this for initialization
	void Start () {
		Transform t;
		if (t = GetComponent<Transform>()) {
			center = t.position;
		}

		for (int i = 0; i < copies; i++) {
			Vector3 point = Random.onUnitSphere;
			point.z += Random.Range(-maxHeightDiff/2, maxHeightDiff/2);
			GameObject obj = Instantiate(source, point*radius + center, source.transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
