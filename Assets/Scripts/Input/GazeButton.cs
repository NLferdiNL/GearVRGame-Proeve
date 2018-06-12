using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A object waiting for select and deselect messages.
/// </summary>
public class GazeButton : MonoBehaviour {

	/// <summary>
	/// The class used to send the event on finish.
	/// </summary>
	[Serializable]
	public class GazeFinishedEvent : UnityEvent { }

	/// <summary>
	/// The class used to send the every moment the time changes.
	/// </summary>
	[Serializable]
	public class GazeTimeEvent : UnityEvent<float> { }

	/// <summary>
	/// The variable used to listen to the event.
	/// </summary>
	public GazeFinishedEvent OnGazeFinished = new GazeFinishedEvent();

	/// <summary>
	/// Triggered every time the current gaze time changes.
	/// Sends a normalized number based on currTime and maxTime.
	/// </summary>
	public GazeTimeEvent OnGazeTimeChanged = new GazeTimeEvent();

	/// <summary>
	/// How long does the user have to gaze to trigger me.
	/// </summary>
	[SerializeField]
	float maxTime = 3;

	/// <summary>
	/// How long have I been gazed at.
	/// </summary>
	float currTime = 0;

	/// <summary>
	/// Restart when finished, useful for raising numbers and such.
	/// </summary>
	[SerializeField]
	bool looping = false;

	/// <summary>
	/// A getter to quickly calculate the normalized time.
	/// </summary>
	// Normalized means a value between 0 and 1, like 0% and 100%.
	float normalizedCurrTime {
		get {
			return 100 / maxTime * currTime / 100;
		}
	}

	/// <summary>
	/// Am I currently gazed at?
	/// </summary>
	bool active = false;

	/// <summary>
	/// Being gazed at.
	/// </summary>
	public void Selected() {
		active = true;
	}

	/// <summary>
	/// No longer being gazed at.
	/// </summary>
	public void Deselected() {
		active = false;
	}

	/// <summary>
	/// Handles all the events and time based events depending on wether or not I am active.
	/// Sends events if max time is reached and every moment the current time is changed.
	/// </summary>
	private void FixedUpdate() {
		// Am I currently gazed at?
		if(active) {
			// Add time since last frame.
			currTime += Time.deltaTime;

			// Did I pass the maxTime?
			if(currTime >= maxTime) {
				// If I am not a looping button
				// set active to false.
				if(!looping)
					active = false;

				// Set my current time to 0.
				currTime = 0;

				// And notify the listeners.
				OnGazeFinished.Invoke();
			}

			// The value changes, let the listeners know.
			OnGazeTimeChanged.Invoke(normalizedCurrTime);

		// Not active.
		} else if(currTime > 0) {
			// The value is not 0 yet, remove time since last frame.
			currTime -= Time.deltaTime;

			// Value was changed, invoke the event.
			OnGazeTimeChanged.Invoke(normalizedCurrTime);
		} else if(currTime < 0) {
			// Set it to 0 so when I restart it isn't delayed.
			currTime = 0;
		}
	}
}
