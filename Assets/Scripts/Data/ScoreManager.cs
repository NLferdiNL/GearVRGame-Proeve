using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Holds the score and manages access to it.
/// </summary>
public class ScoreManager : MonoBehaviour {

	/// <summary>
	/// The event used to send the latest score.
	/// </summary>
	[System.Serializable]
	public class ScoreChangeEvent : UnityEvent<int> { }

	/// <summary>
	/// Only accesible in the inspector through the component
	/// directly.
	/// </summary>
	[SerializeField]
	ScoreChangeEvent onScoreChanged = new ScoreChangeEvent();

	/// <summary>
	/// To have a static reference to the component.
	/// </summary>
	static ScoreManager instance;

	/// <summary>
	/// To allow components listening to the event without
	/// a reference to the component.
	/// </summary>
	public static ScoreChangeEvent OnScoreChanged {
		get {
			if(instance == null)
				return null;

			return instance.onScoreChanged;
		}
	}

	/// <summary>
	/// A public static integer that can only be modified by this class.
	/// </summary>
	public static int Score { get; private set; }

	/// <summary>
	/// To allows outside components to change the score.
	/// </summary>
	public static void AddToScore(int value) {
		Score += value;
		OnScoreChanged.Invoke(Score);
	}

	/// <summary>
	/// Set up the singleton and score value.
	/// </summary>
	private void Start() {
		Score = 0;
		instance = this;
	}
}
