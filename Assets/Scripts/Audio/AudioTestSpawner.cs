using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestSpawner : MonoBehaviour {

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

					GameObject cube = Instantiate(audioTestPrefab, transform);
					cube.transform.position = cube.transform.forward * .25f * i;
					cube.GetComponent<AudioTest>().range = range;
					//cube.transform.localScale = new Vector3(.25f, 1, .25f);
				}
				break;
			case VisualizerType.StraightMiddleToOuter:
				float middle = width / 2;
				for(int i = 0; i < width; i++) {
					int range = width / 2 - i;

					if(i > width / 2) {
						range = i - width / 2;
					}

					GameObject cube = Instantiate(audioTestPrefab, transform);
					cube.transform.position = cube.transform.forward * .25f * i;
					cube.GetComponent<AudioTest>().range = range;
					//cube.transform.localScale = new Vector3(.25f, 1, .25f);
				}
				break;
		}
	}
}
