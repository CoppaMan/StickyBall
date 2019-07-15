using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatamariView : MonoBehaviour {

	Camera view;
	Objective objective;

	// Use this for initialization
	void Start () {
		view = gameObject.GetComponent<Camera>();
		objective = GameObject.FindGameObjectWithTag("Level").GetComponent<Objective>();
		view.orthographicSize = ((float) objective.targetSizeMM) / ((float) objective.startSizeMM * 2f);
	}
}
