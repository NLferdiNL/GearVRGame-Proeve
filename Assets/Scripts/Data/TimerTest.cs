using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A example showing how to properly use the Timer class.
/// </summary>
public class TimerTest : MonoBehaviour {

	/// <summary>
	/// A variable to contain the timer object.
	/// </summary>
	Timer timer;

	/// <summary>
	/// A reference to the text object I am changing.
	/// </summary>
	[SerializeField]
	Text text;

	/// <summary>
	/// Set up all variables, listeners and start the timer.
	/// </summary>
	private void Start() {
		// Create the timer.
		timer = new Timer();

		timer.Start();

		// Add the listener.
		timer.OnTick += OnTick;

		// If I wasn't assigned a text object, check if my gameObject has one.
		if(text == null)
			text = GetComponent<Text>();
	}

	/// <summary>
	/// To be used by the Timer object.
	/// </summary>
	/// <param name="sender"></param>
	private void OnTick(Timer sender) {
		text.text = sender.ToString();
	}

	/// <summary>
	/// Make sure its cleaned up.
	/// </summary>
	private void OnApplicationQuit() {
		Dispose();	
	}

	/// <summary>
	/// If the object gets destroyed, clean up.
	/// </summary>
	private void OnDestroy() {
		Dispose();
	}

	/// <summary>
	/// The Timer object makes use of a class that cannot be cleaned up automatically.
	/// Make sure it is disposed to prevent leakage.
	/// </summary>
	private void Dispose() {
		timer.Dispose();
	}
}
