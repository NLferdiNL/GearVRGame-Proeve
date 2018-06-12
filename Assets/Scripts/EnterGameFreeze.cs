using System.Collections;
using UnityEngine;

/// <summary>
/// Freeze game on start.
/// </summary>
public class EnterGameFreeze : MonoBehaviour {

	public float timeToFreeze = 5;

    public void PauseGame()
    {
        StartCoroutine(Pause());
    }

    IEnumerator Pause()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(timeToFreeze);
        Time.timeScale = 1;
    }
}
