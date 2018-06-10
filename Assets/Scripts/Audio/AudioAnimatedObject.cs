using UnityEngine;

/// <summary>
/// Influences the animatior normalizedTime based on the AudioData.GetFloat(range) height * amplify.
/// To create a smoother animation it uses MoveTowards with barSpeed so the speed can be edited in
/// the inspector.
/// </summary>
public class AudioAnimatedObject : MonoBehaviour {

	// To SetFloat the normalizedTime.
	Animator animator;

	// What range on the AudioData?
	public int range = 0;

	// How strong will the float from AudioData be?
	public float amplify = 2;

	// How fast can the animation move?
	public float barSpeed = 4;

	// To animate the Material color.
	public Color color = Color.red;

	// To grab the material.
	[SerializeField]
	MeshRenderer meshRenderer;

	// To store a reference to the instance of the material
	// we're editing.
	Material mat;

	// If no MeshRenderer was set, this is false to prevent errors.
	bool meshRendererAvailable {
		get {
			return meshRenderer != null;
		}
	}

	private void Start() {
		animator = GetComponent<Animator>();
		if(meshRendererAvailable) {
			mat = new Material(meshRenderer.material);
			meshRenderer.material = mat;
		}
	}

	private void FixedUpdate() {
		animator.SetFloat("time", Mathf.MoveTowards(animator.GetFloat("time"), AudioData.GetFloat(range) * amplify, Time.deltaTime * barSpeed));
		if(meshRendererAvailable)
			mat.color = color;
	}
}
