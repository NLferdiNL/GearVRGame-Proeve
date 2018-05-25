using System.Collections;
using UnityEngine;

public class EnterGameFreeze : MonoBehaviour {
	
	IEnumerator Start () {
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(5);
		Time.timeScale = 1;
	}
}
