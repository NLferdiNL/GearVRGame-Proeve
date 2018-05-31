using UnityEngine;

public class GazeInput : MonoBehaviour {

	[SerializeField]
	LayerMask gazeLayerMask;

	[SerializeField]
	Transform gazeSource;

	[SerializeField]
	Transform gazeReticle;

	[SerializeField]
	float gazeRange = 100;

	Collider currentTarget = null;

	private void FixedUpdate() {
		Ray ray = new Ray(gazeSource.position, (gazeReticle.position - gazeSource.position).normalized);

		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, gazeRange, gazeLayerMask)) {
			if(hit.collider == currentTarget) {
				OnRaycastStay(currentTarget);
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
	}

	private void OnRaycastExit(Collider other) {
		other.SendMessage("Deselected", SendMessageOptions.DontRequireReceiver);
	}
}
