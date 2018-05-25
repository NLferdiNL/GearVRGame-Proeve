using System;
using UnityEngine;
using UnityEngine.Events;

public class GazeButton : MonoBehaviour {

	[Serializable]
	public class GazeFinishedEvent : UnityEvent { }

	[Serializable]
	public class GazeTimeEvent : UnityEvent<float> { }

	public GazeFinishedEvent OnGazeFinished = new GazeFinishedEvent();

	public GazeTimeEvent OnGazeTimeChanged = new GazeTimeEvent();

	[SerializeField]
	float maxTime = 3;

	float currTime = 0;

	[SerializeField]
	bool looping = false;

	float normalizedCurrTime {
		get {
			return 100 / maxTime * currTime / 100;
		}
	}

	bool active = false;

	public void Selected() {
		active = true;
	}

	public void Deselected() {
		active = false;
	}

	private void FixedUpdate() {
		if(active) {
			currTime += Time.deltaTime;
			OnGazeTimeChanged.Invoke(normalizedCurrTime);

			if(currTime >= maxTime) {
				if(!looping)
					active = false;
				else
					currTime = 0;
				OnGazeFinished.Invoke();
			}
			
		} else if(currTime > 0) {
			currTime -= Time.deltaTime;
			OnGazeTimeChanged.Invoke(normalizedCurrTime);
		} else if(currTime < 0) {
			currTime = 0;
		}
	}
}
