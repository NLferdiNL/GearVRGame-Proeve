using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	[SerializeField]
	Text textUI;

	int lastScore = 0;

	public void OnScoreChange(int value) {
		StopAllCoroutines();
		StartCoroutine(SlowlyGoUp(value));
	}

	IEnumerator SlowlyGoUp(int value) {
		float t = 0;
		int start = lastScore;

		while(t < 1) {
			t += Time.deltaTime * 10;
			lastScore = (int)Mathf.Lerp(start, value, t);
			textUI.text = lastScore.ToString("D4");

			yield return null;
		}
	}
}
