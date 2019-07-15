using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float currentTime = 0;
	private Image hand;
	private Text display;
	private Vector3 handPosition;
	private bool decreasing;
	float buffer = 0;
	public bool active;

	void Start () {
		hand = GameObject.Find("Hand").GetComponent<Image>();
		display = GameObject.Find("Minutes").GetComponent<Text>();
		Objective obj = GameObject.FindGameObjectWithTag("Level").GetComponent<Objective>();
		decreasing = obj.hasTargetTime;
		if (decreasing) {
			buffer = (float) obj.targetTimeSec;
		}
		handPosition = hand.rectTransform.position;
	}
	
	void Update () {
		if (active) {
			buffer += (decreasing ? -1 : 1) * Time.deltaTime;
			buffer = buffer < 0 ? 0 : buffer;

			int minutes = Mathf.FloorToInt(buffer) / 60;
			float seconds = buffer % 60;

			hand.rectTransform.position = handPosition;
			hand.rectTransform.rotation = Quaternion.identity;
			hand.rectTransform.RotateAround(hand.rectTransform.position - hand.rectTransform.localPosition, Vector3.forward, -(seconds*360)/60);

			if (buffer <= 60) {
				display.fontSize = 70;
				display.rectTransform.parent.GetComponent<Image>().rectTransform.sizeDelta = Vector3.one*100;
				display.text = decreasing ? Mathf.CeilToInt(buffer).ToString() : Mathf.FloorToInt(buffer).ToString();
			} else {
				display.fontSize = 40;
				display.rectTransform.parent.GetComponent<Image>().rectTransform.sizeDelta = Vector3.one*80;
				display.text = minutes.ToString();
			}
		}
		//Debug.Log(buffer);
	}

	public bool IsZero() {
		if (Mathf.CeilToInt(buffer) == 0) {
			return true;
		}
		return false;
	}
}
