using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestSpawner : MonoBehaviour {

	[SerializeField]
	int width = 100;

	[SerializeField]
	GameObject audioTestPrefab;

	void Start () {
		for(int i = 0; i < width; i++) {
			int range = i;

			if(i > width / 2)
				range = width - i;

			GameObject cube = Instantiate(audioTestPrefab, transform);
			cube.transform.position = cube.transform.forward * .25f * i;
			cube.GetComponent<AudioTest>().range = range;
			//cube.transform.localScale = new Vector3(.25f, 1, .25f);
		}
	}
}
