using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Image toggle.
/// Since UnityEvents can't touch them.
/// </summary>
public class ToggleColor : MonoBehaviour {

	/// <summary>
	/// Where will I apply the color changes?
	/// </summary>
	[SerializeField]
	Image image;

	/// <summary>
	/// What are my on off colors?
	/// </summary>
	[SerializeField]
	Color on, off;

	/// <summary>
	/// Public way to call on.
	/// </summary>
	public void On() {
		image.color = on;
	}

	/// <summary>
	/// Public way to call off.
	/// </summary>
	public void Off() {
		image.color = off;
	}
}
