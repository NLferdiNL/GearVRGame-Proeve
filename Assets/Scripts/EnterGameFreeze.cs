using System.Collections;
using UnityEngine;

/// <summary>
/// Freeze game on start.
/// </summary>
public class EnterGameFreeze : MonoBehaviour {

	[SerializeField]
	float timeToFreeze = 5;

	IEnumerator Start () {
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(timeToFreeze);
		Time.timeScale = 1;
	}
}
