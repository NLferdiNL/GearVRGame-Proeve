using UnityEngine;

/// <summary>
/// Used to easily create a visualizer.
/// Can be extended to allow more visualizer types.
/// </summary>
public class AudioVisualizerSpawner : MonoBehaviour {

	// How big will it be?
	[SerializeField]
	int width = 500;

	// The divider used by the modulo to have multiple visulizer prefabs be the same range.
	[SerializeField]
	int moduloDivider = 2;

	// A reference to the prefab used to instantiate all visualizers.
	[SerializeField]
	GameObject audioVisualizerPrefab;

	// The type we will be spawned.
	[SerializeField]
	VisualizerType visualizerType = VisualizerType.StraightOuterToMiddle;

	// A list of VisualizerTypes,
	// enum to make it easier to understand
	// what you're selecting.
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

	// The instantiating and placing of the prefabs is identical for each VisualizerType
	// so having it seperate from the for loops in Start() is much better.
	GameObject CreateObject(Vector3 localPosition, Quaternion localRotation, int range) {
		GameObject obj = Instantiate(audioVisualizerPrefab, transform);
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
