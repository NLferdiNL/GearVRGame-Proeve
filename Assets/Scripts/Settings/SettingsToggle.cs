using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
