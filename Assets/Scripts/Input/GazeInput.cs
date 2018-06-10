using UnityEngine;

/// <summary>
/// Sends all the gaze related messages.
/// </summary>
public class GazeInput : MonoBehaviour {
	
	// What can I gaze at?
	[SerializeField]
	LayerMask gazeLayerMask;

	// The source of the gaze ray, usually the camera.
	[SerializeField]
	Transform gazeSource;

	// The direction of the gaze ray, usually a reticle
	// with smooth follow.
	[SerializeField]
	Transform gazeReticle;

	// How far can I gaze?
	[SerializeField]
	float gazeRange = 100;

	// Currently gazing at?
	Collider currentTarget = null;

	private void FixedUpdate() {
		Ray ray = new Ray(gazeSource.position, (gazeReticle.position - gazeSource.position).normalized);

		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, gazeRange, gazeLayerMask)) {
			if(hit.collider == currentTarget) {
				//OnRaycastStay(currentTarget);
			} else {
				if(currentTarget != null) {
					OnRaycastExit(currentTarget);
                    currentTarget = null;
                }

                currentTarget = hit.collider;
				OnRaycastEnter(currentTarget);
			}
		} else {
			if(currentTarget != null) {
				OnRaycastExit(currentTarget);
                currentTarget = null;
            }
        }
	}

	private void OnRaycastEnter(Collider other) {
		other.SendMessage("Selected", SendMessageOptions.DontRequireReceiver);
	}

	private void OnRaycastStay(Collider other) {
		// Do nothing.
		// Only here if I need it to do something.
	}

	private void OnRaycastExit(Collider other) {
		other.SendMessage("Deselected", SendMessageOptions.DontRequireReceiver);
	}
}
