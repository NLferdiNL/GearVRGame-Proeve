using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour {

	[System.Serializable]
	public class ScoreChangeEvent : UnityEvent<int> { }

	[SerializeField]
	ScoreChangeEvent onScoreChanged = new ScoreChangeEvent();

	static ScoreManager instance;

	public static ScoreChangeEvent OnScoreChanged {
		get {
			return instance.onScoreChanged;
		}
	}

	public static int Score { get; private set; }

	public static void EditScore(int value) {
		Score += value;
		OnScoreChanged.Invoke(Score);
	}

	private void Start() {
		Score = 0;
		instance = this;
	}
}
