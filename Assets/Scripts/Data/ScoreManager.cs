using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour {

	// The event used to send the latest score.
	[System.Serializable]
	public class ScoreChangeEvent : UnityEvent<int> { }

	// Only accesible in the inspector through the component
	// directly.
	[SerializeField]
	ScoreChangeEvent onScoreChanged = new ScoreChangeEvent();

	// To have a static reference to the component.
	static ScoreManager instance;

	// To allow components listening to the event without
	// a reference to the component.
	public static ScoreChangeEvent OnScoreChanged {
		get {
			if(instance == null)
				return null;

			return instance.onScoreChanged;
		}
	}

	// A public static integer that can only be modified by this class.
	public static int Score { get; private set; }

	// To allows outside components to change the score.
	public static void AddToScore(int value) {
		Score += value;
		OnScoreChanged.Invoke(Score);
	}

	private void Start() {
		Score = 0;
		instance = this;
	}
}
