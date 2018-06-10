using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A example showing how to properly use the Timer class.
/// </summary>
public class TimerTest : MonoBehaviour {

	Timer timer;
	Text text;

	private void Start() {
		timer = new Timer();

		timer.Start();

		timer.OnTick += OnTick;

		text = GetComponent<Text>();
	}

	private void OnTick(Timer sender) {
		text.text = sender.ToString();
	}

	// Make sure its cleaned up.
	private void OnApplicationQuit() {
		Dispose();	
	}

	private void OnDestroy() {
		Dispose();
	}

	private void Dispose() {
		timer.Dispose();
	}
}
