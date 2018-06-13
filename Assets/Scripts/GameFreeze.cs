using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Freeze game on call.
/// </summary>
public class GameFreeze : MonoBehaviour {

    /// <summary>
    /// Public instance to call my methods.
    /// </summary>
    public GameFreeze instance;

	/// <summary>
	/// Set up the singleton for calling me.
	/// </summary>
	void Start() {
		instance = this;
	}
    
	/// <summary>
	/// A public method to stop time until condition is met.
	/// </summary>
	/// <param name="condition">A condition that needs to be met before restarting time.</param>
	/// <returns></returns>
	public IEnumerator PauseUntil(Func<bool> condition) {
		// Stop time.
		Time.timeScale = 0.25f;

		// And wait until the condition is met.
		yield return new WaitUntil(condition);

		// Restart time.
		Time.timeScale = 1;
	}

	/// <summary>
	/// Pause until time to freeze has passed.
	/// </summary>
	/// <param name="timeToFreeze"></param>
	/// <returns></returns>
	public IEnumerator PauseFor(float timeToFreeze) {
		// Set the time we need to meet.
		float time = Time.timeSinceLevelLoad + timeToFreeze;

		// And then make a lambda that checks that it has paused
		// so PauseUntil can use it.
		yield return PauseUntil(() => time <= Time.timeSinceLevelLoad);

	}
}
