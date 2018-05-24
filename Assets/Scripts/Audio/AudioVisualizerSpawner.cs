using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizerSpawner : MonoBehaviour {

	[SerializeField]
	int width = 500;

	[SerializeField]
	int moduloDivider = 2;

	[SerializeField]
	GameObject audioTestPrefab;

	[SerializeField]
	VisualizerType visualizerType = VisualizerType.StraightOuterToMiddle;

	public enum VisualizerType {
		StraightOuterToMiddle = 0,
		StraightMiddleToOuter = 1
	}

	void Start() {
		switch(visualizerType) {
			case VisualizerType.StraightOuterToMiddle:
				for(int i = 0; i < width; i++) {
					int range = i - i % moduloDivider;

					if(i > width / 2) {
						range = width - i;
						range = range - range % moduloDivider;
					}

					CreateObject(transform.forward * .25f * i, Quaternion.identity, range);
				}
				break;
			case VisualizerType.StraightMiddleToOuter:
				float middle = width / 2;
				for(int i = 0; i < width; i++) {
					int range = width / 2 - i;

					if(i > width / 2) {
						range = i - width / 2;
					}

					CreateObject(transform.forward * .25f * i, Quaternion.identity, range);
				}
				break;
		}
	}

	GameObject CreateObject(Vector3 localPosition, Quaternion localRotation, int range) {
		GameObject obj = Instantiate(audioTestPrefab, transform);
		obj.transform.localPosition = localPosition;
		obj.transform.localRotation = localRotation;

		AudioAnimatedObject audioAnimated = obj.GetComponent<AudioAnimatedObject>();

		if(audioAnimated != null)
			audioAnimated.range = range;
		else
			Debug.LogError("No AudioAnimatedObject found!");

		return obj;
	}
}
