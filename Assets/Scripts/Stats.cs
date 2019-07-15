using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

	public string ObjectName;
	public float length;
	public float ObjectVolume;

	// Use this for initialization
	void Start () {
		gameObject.name = ObjectName;

		ObjectVolume *= gameObject.transform.localScale.x;
		ObjectVolume *= gameObject.transform.localScale.y;
		ObjectVolume *= gameObject.transform.localScale.z;
	}

	public void SetObjectName(string name) {
		ObjectName = name;
	}

	public float GetVolume() {
		return ObjectVolume;
	}
}
