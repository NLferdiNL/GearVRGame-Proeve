using UnityEngine;
using UnityEngine.PostProcessing;

/// <summary>
/// Changes a PostProcessing setting based on the influence list.
/// Allows extension for more post settings when neccesary.
/// </summary>
public class GraphicsToggle : MonoBehaviour {
	
	public enum GraphicsSettings {
		Bloom = 0
	}

	[SerializeField]
	GraphicsSettings[] toInfluence;

	public void Toggle(bool value) {
		for(int i = 0; i < toInfluence.Length; i++) {
			switch(toInfluence[i]) {
				case GraphicsSettings.Bloom:
					GetComponent<PostProcessingBehaviour>().profile.bloom.enabled = value;
					break;
			}
		}
	}
}
