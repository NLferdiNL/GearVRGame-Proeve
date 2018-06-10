using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A object waiting for select and deselect messages.
/// </summary>
public class GazeButton : MonoBehaviour {

	[Serializable]
	public class GazeFinishedEvent : UnityEvent { }

	[Serializable]
	public class GazeTimeEvent : UnityEvent<float> { }

	public GazeFinishedEvent OnGazeFinished = new GazeFinishedEvent();

	// Triggered every time the current gaze time changes.
	// Sends a normalized number based on currTime and maxTime.
	public GazeTimeEvent OnGazeTimeChanged = new GazeTimeEvent();

	// How long does the user have to gaze to trigger me.
	[SerializeField]
	float maxTime = 3;

	// How long have I been gazed at.
	float currTime = 0;

	// Restart when finished, useful for raising numbers and such.
	[SerializeField]
	bool looping = false;

	float normalizedCurrTime {
		get {
			return 100 / maxTime * currTime / 100;
		}
	}

	// Am I currently gazed at?
	bool active = false;

	// Gaze on.
	public void Selected() {
		active = true;
	}

	// Gaze off.
	public void Deselected() {
		active = false;
	}

	private void FixedUpdate() {
		if(active) {
			currTime += Time.deltaTime;

			if(currTime >= maxTime) {
				if(!looping)
					active = false;

				currTime = 0;
				OnGazeFinished.Invoke();
			}

			OnGazeTimeChanged.Invoke(normalizedCurrTime);

		} else if(currTime > 0) {
			currTime -= Time.deltaTime;
			OnGazeTimeChanged.Invoke(normalizedCurrTime);
		} else if(currTime < 0) {
			currTime = 0;
		}
	}
}
