using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour {

	public int startSizeMM = 50;
	public int targetSizeMM = 150;
	public bool hasTargetTime;
	public bool hasTargetSize;
	public int targetTimeSec;
	public Object scene;
	public Mesh katamari;
}