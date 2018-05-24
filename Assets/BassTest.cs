using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassTest : MonoBehaviour {

	[SerializeField]
	int rangeToPick = 4;

	[SerializeField]
	float range;

	void Update () {
		range = AudioData.GetFloat(rangeToPick);
		if(range > .5f) {
			print("UMPFH");
		}
	}
}
