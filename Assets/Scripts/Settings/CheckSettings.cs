using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Checks  if a settings flag exists and reacts accordingly.
/// </summary>
public class CheckSettings : MonoBehaviour {

	[SerializeField]
	string value;

	public UnityEvent OnTrue = new UnityEvent();
	public UnityEvent OnFalse = new UnityEvent();
	
	private void Start() {
		if(!string.IsNullOrEmpty(value)) {
			if(GameSettings.Has(value)) {
				OnTrue.Invoke();
			} else {
				OnFalse.Invoke();
			}
		}
	}
}
