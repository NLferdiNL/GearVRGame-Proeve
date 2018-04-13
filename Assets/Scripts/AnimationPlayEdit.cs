using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayEdit : MonoBehaviour {
	[SerializeField]
	Animation anim;

	[SerializeField, Range(0,1)]
	float time;

	AnimationState state;

	private void Start() {
		state = anim["testanim"];
		state.enabled = true;
		state.weight = 1;
	}

	private void FixedUpdate() {
		state.normalizedTime = time;
		state.enabled = true;
		anim.Sample();
		state.enabled = false;
	}
}
