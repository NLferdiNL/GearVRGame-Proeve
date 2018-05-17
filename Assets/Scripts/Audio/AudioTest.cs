using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour {

	Animator animator;

	public int range = 0;

	public float amplify = 2;

	public float barSpeed = 4;

	public Color color = Color.red;

	[SerializeField]
	MeshRenderer meshRenderer1, meshRenderer2;

	Material mat;

	private void Start() {
		animator = GetComponent<Animator>();
		mat = new Material(meshRenderer1.material);
		meshRenderer1.material = meshRenderer2.material = mat;
	}

	private void FixedUpdate() {
		animator.SetFloat("time", Mathf.MoveTowards(animator.GetFloat("time"), AudioData.GetFloat(range) * amplify, Time.deltaTime * barSpeed));
		mat.color = color;
	}
}
