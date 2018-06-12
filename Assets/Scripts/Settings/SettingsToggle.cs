using UnityEngine;

/// <summary>
/// Toggle a game settings flag. 
/// </summary>
public class SettingsToggle : MonoBehaviour {

	/// <summary>
	/// What flag am I changing?
	/// </summary>
	[SerializeField]
	string value;

	/// <summary>
	/// Public a way to enable.
	/// </summary>
	public void Enable() {
		GameSettings.Set(value);
	}
	
	/// <summary>
	/// Public way to disable.
	/// </summary>
	public void Disable() {
		GameSettings.Remove(value);
	}
}
