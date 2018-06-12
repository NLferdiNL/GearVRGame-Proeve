using UnityEngine;
using UnityEngine.PostProcessing;

/// <summary>
/// Changes a PostProcessing setting based on the influence list.
/// Allows extension for more post settings when neccesary.
/// </summary>
public class GraphicsToggle : MonoBehaviour {
	
	/// <summary>
	/// The enum list of available, can be expanded to allow more.
	/// </summary>
	public enum GraphicsSettings {
		Bloom = 0
	}

	/// <summary>
	/// An array of options to influence at trigger.
	/// </summary>
	[SerializeField]
	GraphicsSettings[] toInfluence;

	/// <summary>
	/// To trigger the option changes.
	/// </summary>
	/// <param name="value"></param>
	public void Toggle(bool value) {
		// For all the items in toInfluence
		for(int i = 0; i < toInfluence.Length; i++) {
			// check which it is.
			switch(toInfluence[i]) {
				// And set accordingly.
				case GraphicsSettings.Bloom:
					GetComponent<PostProcessingBehaviour>().profile.bloom.enabled = value;
					break;
			}
		}
	}
}
