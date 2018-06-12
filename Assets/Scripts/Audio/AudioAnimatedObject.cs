using UnityEngine;

/// <summary>
/// Influences the animatior normalizedTime based on the AudioData.GetFloat(range) height * amplify.
/// To create a smoother animation it uses MoveTowards with barSpeed so the speed can be edited in
/// the inspector.
/// </summary>
public class AudioAnimatedObject : MonoBehaviour {

	/// <summary>
	/// To SetFloat the normalizedTime.
	/// </summary>
	Animator animator;

	/// <summary>
	/// What range on the AudioData?
	/// </summary>
	public int range = 0;

	/// <summary>
	/// How strong will the float from AudioData be?
	/// </summary>
	public float amplify = 2;

	/// <summary>
	/// How fast can the animation move?
	/// </summary>
	public float barSpeed = 4;

	/// <summary>
	/// To animate the Material color.
	/// </summary>
	public Color color = Color.red;

	/// <summary>
	/// To grab the material.
	/// </summary>
	[SerializeField]
	MeshRenderer meshRenderer;

	/// <summary>
	/// To store a reference to the instance of the material
	/// we're editing.
	/// </summary>
	Material mat;

	/// <summary>
	/// If no MeshRenderer was set, this is false to prevent errors.
	/// </summary>
	bool meshRendererAvailable {
		get {
			return meshRenderer != null;
		}
	}

	/// <summary>
	/// Set up the animator and check if there is a MeshRenderer available
	/// to edit the material of.
	/// </summary>
	private void Start() {
		animator = GetComponent<Animator>();
		if(meshRendererAvailable) {
			mat = new Material(meshRenderer.material);
			meshRenderer.material = mat;
		}
	}

	/// <summary>
	/// Gets the current volume at range, amplfies it by amplfy and gives it to the animator.
	/// Sets the material's color too.
	/// </summary>
	private void FixedUpdate() {
		animator.SetFloat("time", Mathf.MoveTowards(animator.GetFloat("time"), AudioData.GetFloat(range) * amplify, Time.deltaTime * barSpeed));
		if(meshRendererAvailable)
			mat.color = color;
	}
}
