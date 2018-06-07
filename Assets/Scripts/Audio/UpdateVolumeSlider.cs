using UnityEngine;
using UnityEngine.UI;

public class UpdateVolumeSlider : MonoBehaviour {
	private Slider slider;

	[SerializeField]
	private AudioVolumeHolder audioVolumeHolder;

	[SerializeField]
	private SoundType influences;

	public enum SoundType {
		SFX = 0,
		Music = 1
	}

	private void Start() {
		slider = GetComponent<Slider>();
		switch(influences) {
			case SoundType.Music:
				audioVolumeHolder.OnMusicChange.AddListener(OnVolumeUpdate);
				break;
			case SoundType.SFX:
				audioVolumeHolder.OnSFXChange.AddListener(OnVolumeUpdate);
				break;
			default:
				Debug.Log("Something is wrong");
				break;
		}
	}

	private void OnVolumeUpdate(float newVal) {
		switch(influences) {
			case SoundType.Music:
				slider.value = newVal;
				break;
			case SoundType.SFX:
				slider.value = newVal;
				break;
			default:
				Debug.Log("Something is wrong");
				break;
		}
	}
}
