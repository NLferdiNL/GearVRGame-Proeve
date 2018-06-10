using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Image toggle.
/// Since UnityEvents can't touch them.
/// </summary>
public class ToggleColor : MonoBehaviour {

	[SerializeField]
	Image image;

	[SerializeField]
	Color on, off;

	public void On() {
		image.color = on;
	}

	public void Off() {
		image.color = off;
	}
}
