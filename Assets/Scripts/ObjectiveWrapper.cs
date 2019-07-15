using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectiveWrapper : MonoBehaviour {
	enum State {Running, Success, Failed};
	private int startSizeMM;
	private int targetSizeMM;
	private bool hasTargetTime;
	private bool hasTargetSize;
	private int targetTimeSec;
	private Stats katamari;
	private Timer timer;
	private Control control;
	private Objective objective;
	private State state;
	void Start () {
		katamari = GameObject.FindGameObjectWithTag("Katamari").GetComponent<Stats>();
		timer = GameObject.Find("Timer").GetComponent<Timer>();
		control = GameObject.FindGameObjectWithTag("Katamari").GetComponent<Control>();
		objective = GameObject.FindGameObjectWithTag("Level").GetComponent<Objective>();
		state = State.Running;
	}

	public bool HasTargetSize() {
		return objective.hasTargetSize;
	}

	public bool HasTargetTime() {
		return objective.hasTargetTime;
	}

	public int TargetSizeMM() {
		return objective.targetSizeMM;
	}

	public int TargetTimeSec() {
		return objective.targetTimeSec;
	}

	public int StartSizeMM() {
		return objective.startSizeMM;
	}

	void Update () {
		if ((state == State.Running) && ((HasTargetSize() && katamari.length*100 >= TargetSizeMM()) ||
			(HasTargetTime() && timer.IsZero()))) {
				state = State.Success;
			control.movable = false;
			timer.active = false;
			GameObject.Find("Message").GetComponent<Text>().enabled = true;
			Destroy(gameObject.GetComponent<Rigidbody>());
			Object.DontDestroyOnLoad(gameObject);
			SceneManager.LoadScene("Result");
			GameObject.Find("KatamariView").GetComponent<Camera>().orthographicSize = 1;
			gameObject.AddComponent<>()
		}
	}
}
