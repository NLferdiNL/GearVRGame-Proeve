using UnityEngine;

/// <summary>
/// Used to easily create a visualizer.
/// Can be extended to allow more visualizer types.
/// </summary>
public class AudioVisualizerSpawner : MonoBehaviour {

	/// <summary>
	/// How big will it be?
	/// </summary>
	[SerializeField]
	int width = 500;

	/// <summary>
	/// The divider used by the modulo to have multiple visulizer prefabs be the same range.
	/// </summary>
	[SerializeField]
	int moduloDivider = 2;

	/// <summary>
	/// A reference to the prefab used to instantiate all visualizers.
	/// </summary>
	[SerializeField]
	GameObject audioVisualizerPrefab;

	/// <summary>
	/// The type we will be spawned.
	/// </summary>
	[SerializeField]
	VisualizerType visualizerType = VisualizerType.StraightOuterToMiddle;

	/// <summary>
	/// A list of VisualizerTypes,
	/// enum to make it easier to understand
	/// what you're selecting.
	/// </summary>
	public enum VisualizerType {
		StraightOuterToMiddle = 0,
		StraightMiddleToOuter = 1
	}

	/// <summary>
	/// Gets the selected visualzer typer and
	/// genererates it accordingly.
	/// </summary>
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
	/// <summary>
	/// The instantiating and placing of the prefabs is identical for each VisualizerType
	/// so having it seperate from the for loops in Start() is much better.
	/// </summary>
	/// <param name="localPosition">Position it's set to.</param>
	/// <param name="localRotation">Rotation it's set to.</param>
	/// <param name="range">The range it will listen to.</param>
	/// <returns></returns>
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
