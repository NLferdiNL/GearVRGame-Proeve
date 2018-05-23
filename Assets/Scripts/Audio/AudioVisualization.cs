using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]

public class AudioVisualization : MonoBehaviour {
    AudioSource audioSource;
	[SerializeField]
	int range = 512;
    public static float[] Samples; //The amount of audio frequency the script will read.

    // Use this for initialization
    void Start() {
		Samples = new float[range];
		audioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        GetSpectrumAudioSource();
	}

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(Samples, 0, FFTWindow.Blackman);
    }

}
