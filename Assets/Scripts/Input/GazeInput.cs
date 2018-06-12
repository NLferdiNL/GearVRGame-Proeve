using UnityEngine;

/// <summary>
/// Sends all the gaze related messages.
/// </summary>
public class GazeInput : MonoBehaviour {
	/// <summary>
	/// What can I gaze at?
	/// </summary>
	[SerializeField]
	LayerMask gazeLayerMask;

	/// <summary>
	/// The source of the gaze ray, usually the camera.
	/// </summary>
	[SerializeField]
	Transform gazeSource;

	/// <summary>
	/// The direction of the gaze ray, usually a reticle
	/// with smooth follow.
	/// </summary>
	[SerializeField]
	Transform gazeReticle;

	/// <summary>
	/// How far can I gaze?
	/// </summary>
	[SerializeField]
	float gazeRange = 100;

	/// <summary>
	/// Currently gazing at?
	/// </summary>
	Collider currentTarget = null;

	/// <summary>
	/// Handles the gaze per frame. Checks if it is on the same object, a new one, or none at all.
	/// And react accordingly using OnEnter, OnStay and OnExit.
	/// </summary>
	private void FixedUpdate() {
		// Create the ray to cast.
		Ray ray = new Ray(gazeSource.position, (gazeReticle.position - gazeSource.position).normalized);

		// To store the hit data.
		RaycastHit hit;

		// Shoot the ray and check if it hits something.
		if(Physics.Raycast(ray, out hit, gazeRange, gazeLayerMask)) {
			// Did it hit the current collider?
			if(hit.collider == currentTarget) {
				// It did, send it to OnStay.
				//OnRaycastStay(currentTarget);
			} else {
				// It was not the same target.
				// If I have a currentTarget send that to OnExit.
				if(currentTarget != null) {
					OnRaycastExit(currentTarget);
					// And wipe the reference to it.
                    currentTarget = null;
                }

				// New reference and send it to OnEnter.
                currentTarget = hit.collider;
				OnRaycastEnter(currentTarget);
			}
		} else {
			// Nothing was hit.
			// If I have a currentTarget send it to OnExit and wipe the reference.
			if(currentTarget != null) {
				OnRaycastExit(currentTarget);
                currentTarget = null;
            }
        }
	}

	/// <summary>
	/// Sends the other collider a message that it is being gazed at.
	/// </summary>
	/// <param name="other">Gazed object</param>
	private void OnRaycastEnter(Collider other) {
		other.SendMessage("Selected", SendMessageOptions.DontRequireReceiver);
	}

	/// <summary>
	/// Does nothing.
	/// </summary>
	/// <param name="other">Gazed object</param>
	private void OnRaycastStay(Collider other) {
		// Do nothing.
		// Only here if I need it to do something.
	}

	/// <summary>
	/// Sends the other collider a message that it is no longer being gazed at.
	/// </summary>
	/// <param name="other">Ungazed object</param>
	private void OnRaycastExit(Collider other) {
		other.SendMessage("Deselected", SendMessageOptions.DontRequireReceiver);
	}
}