using UnityEngine;

public class AudioData : MonoBehaviour {

	[Header("MUST BE A POWER OF TWO!")]
	public int audioRange = 256;

	static AudioData instance;

	AudioSource audioSource;

	float[] audioSpectrumData;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();

		audioSpectrumData = new float[audioRange];
		instance = this;
	}

	private void FixedUpdate() {
		audioSource.GetSpectrumData(instance.audioSpectrumData, 0, FFTWindow.Blackman);
	}

	public static float GetFloat(int range) {
		return instance.audioSpectrumData[range];
	}
}
