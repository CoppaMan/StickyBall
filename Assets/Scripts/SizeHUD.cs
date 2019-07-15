using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SizeHUD : MonoBehaviour {

	private Stats stats;
	private List<Text> text;
	private Image ring;
	private Objective objective;
	float max;

	// Use this for initialization
	void Start () {
		objective = GameObject.FindGameObjectWithTag("Level").GetComponent<Objective>();
		stats = GameObject.FindGameObjectWithTag("Katamari").GetComponent<Stats>();

		max = objective.targetSizeMM;
		text = new List<Text>();
		GetComponentsInChildren<Text>(true, text);
		ring = GameObject.Find("Ring").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		int tot = Mathf.FloorToInt(stats.length*100f);
		int cm = tot / 10;
		int m = cm / 100;
		int mm = tot % 10;
		text[0].text = " " + (m != 0 ? m.ToString() : "") + cm.ToString() + "cm" + mm.ToString() + "mm";
		text[1].text = " " + (objective.targetSizeMM/10).ToString() + "cm";
		ring.rectTransform.localScale = Vector3.one * (tot/max <= 1 ? tot/max : 1f);
		ring.rectTransform.rotation *= Quaternion.AngleAxis(2f, ring.rectTransform.forward);
	}
}
