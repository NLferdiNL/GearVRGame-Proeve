using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnimatedObject : MonoBehaviour {

	Animator animator;

	public int range = 0;

	public float amplify = 2;

	public float barSpeed = 4;

	public Color color = Color.red;

	[SerializeField]
	MeshRenderer meshRenderer;

	Material mat;

	bool meshRendererAvailable = false;

	private void Start() {
		animator = GetComponent<Animator>();
		if(meshRenderer != null) {
			mat = new Material(meshRenderer.material);
			meshRenderer.material = mat;
			meshRendererAvailable = true;
		}
	}

	private void FixedUpdate() {
		animator.SetFloat("time", Mathf.MoveTowards(animator.GetFloat("time"), AudioData.GetFloat(range) * amplify, Time.deltaTime * barSpeed));
		if(meshRendererAvailable)
			mat.color = color;
	}
}
