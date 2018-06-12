using UnityEngine;

/// <summary>
/// Get the SpectrumData from an AudioSource and allows other components to listen to it.
/// </summary>
public class AudioData : MonoBehaviour {

	// How big will our spectrum array be?
	[Header("MUST BE A POWER OF TWO!")]
	public int audioRange = 256;

	// A private reference to this component,
	// only used internally.
	static AudioData instance;

	// A reference to the component we're getting
	// the spectrum data from.
	AudioSource audioSource;

	// Holds the data.
	float[] audioSpectrumData;

	void Awake () {
		audioSource = GetComponent<AudioSource>();

		audioSpectrumData = new float[audioRange];
		instance = this;
	}

	private void FixedUpdate() {
		audioSource.GetSpectrumData(instance.audioSpectrumData, 0, FFTWindow.Blackman);
	}

	// The public method to get data from the spectrum.
	// Static so no reference to the component is required
	// to be stored.
	public static float GetFloat(int range) {
        if (instance == null)
        {
            Debug.LogError("AudioData is non existant but being called.");
            return 0;
        }

		return instance.audioSpectrumData[range];
	}
}
