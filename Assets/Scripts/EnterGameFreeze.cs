using System.Collections;
using UnityEngine;

/// <summary>
/// Freeze game on start.
/// </summary>
public class EnterGameFreeze : MonoBehaviour {

	/// <summary>
	/// How long will the game be paused before start.
	/// </summary>
	[SerializeField]
	float timeToFreeze = 5;

	/// <summary>
	/// Stop game, until time passed then restart.
	/// </summary>
	/// <returns></returns>
	IEnumerator Start () {
		// Freeze time.
		Time.timeScale = 0;

		// Wait for the delay.
		yield return new WaitForSecondsRealtime(timeToFreeze);

		// Restart time.
		Time.timeScale = 1;
	}
}
