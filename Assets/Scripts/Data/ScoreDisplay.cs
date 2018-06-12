using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A component to listen to the ScoreManager for changes
/// and slowly go up.
/// </summary>
public class ScoreDisplay : MonoBehaviour {

	/// <summary>
	/// 
	/// Where am I sending the text changes to?
	/// </summary>
	[SerializeField]
	Text textUI;

	/// <summary>
	/// In case the score changes again, don't
	/// instantly shoot there, store the last
	/// score here to continue lerping.
	/// </summary>
	int lastScore = 0;

	/// <summary>
	/// What will be hooked to the ScoreManager.OnScoreChanged event.
	/// </summary>
	public void OnScoreChange(int value) {
		// In case I was still counting stop it.
		StopAllCoroutines();

		// And start a new coroutine.
		StartCoroutine(SlowlyGoUp(value));
	}

	/// <summary>
	/// The Coroutine that will animate the text.
	/// </summary>
	/// <param name="value">Go to this value.</param>
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
