using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerTest : MonoBehaviour {

	Timer timer;
	Text text;

	private void Start() {
		timer = new Timer();

		timer.Start();
		timer.Start();
		text = GetComponent<Text>();
	}

	private void FixedUpdate() {
		text.text = timer.ToString();
	}

	private void OnApplicationQuit() {
		timer.Dispose();
	}
}
