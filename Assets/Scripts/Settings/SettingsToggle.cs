using UnityEngine;

// Toggle a game settings flag.
public class SettingsToggle : MonoBehaviour {

	[SerializeField]
	string value;

	public void Enable() {
		GameSettings.Set(value);
	}
	
	public void Disable() {
		GameSettings.Remove(value);
	}
}
