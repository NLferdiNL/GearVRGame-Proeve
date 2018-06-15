using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Checks  if a settings flag exists and reacts accordingly.
/// </summary>
public class CheckSettings : MonoBehaviour {

	/// <summary>
	/// The value to check.
	/// </summary>
	[SerializeField]
	string value;

	/// <summary>
	/// If it exists this triggers.
	/// </summary>
	public UnityEvent OnTrue = new UnityEvent();

	/// <summary>
	/// If it does not exist this triggers.
	/// </summary>
	public UnityEvent OnFalse = new UnityEvent();
	
	/// <summary>
	/// Checks if the setting exists and triggers the right event.
	/// </summary>
	private void Start() {
		// Prevents checking for empty value.
		if(!string.IsNullOrEmpty(value)) {
			// Check if it exists.
			if(GameSettings.Has(value)) {
				// It does, invoke true event.
				OnTrue.Invoke();
			} else {
				// It does not, invoke false event.
				OnFalse.Invoke();
			}
		}
	}
}
