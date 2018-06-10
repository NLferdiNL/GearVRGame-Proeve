using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A component to listen to the ScoreManager for changes
/// and slowly go up.
/// </summary>
public class ScoreDisplay : MonoBehaviour {

	// Where am I sending the text changes to?
	[SerializeField]
	Text textUI;

	// In case the score changes again, don't
	// instantly shoot there, store the last
	// score here to continue lerping.
	int lastScore = 0;

	// What will be hooked to the ScoreManager.OnScoreChanged event.
	public void OnScoreChange(int value) {
		// In case I was still counting stop it.
		StopAllCoroutines();

		// And start a new coroutine.
		StartCoroutine(SlowlyGoUp(value));
	}

	// The Coroutine that will animate the text.
	IEnumerator SlowlyGoUp(int value) {
		// Lerp time.
		float t = 0;

		// The last score before I started.
		int start = lastScore;

		while(t < 1) {
			// Add deltaTime to lerp time.
			t += Time.deltaTime * 10;

			// Set lastScore according to Lerp.
			lastScore = (int)Mathf.Lerp(start, value, t);

			// And send it to the UI.
			textUI.text = lastScore.ToString("D4");

			yield return null;
		}
	}
}
