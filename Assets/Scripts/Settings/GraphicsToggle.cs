using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

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
