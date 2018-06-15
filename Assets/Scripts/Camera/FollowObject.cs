using UnityEngine;

/// <summary>
/// Follow a transform.position + transform.forward.
/// Useful for HUDs and reticles.
/// </summary>
public class FollowObject : MonoBehaviour {

	/// <summary>
	/// What am I following?
	/// </summary>
	[SerializeField]
	Transform target;

	/// <summary>
	/// How fast am I following?
	/// </summary>
	[SerializeField]
	float followSpeed = 5;

	/// <summary>
	/// How far forward will I follow?
	/// </summary>
	[SerializeField]
	float distanceForward = 100;

	/// <summary>
	/// Not follow in the Y axis.
	/// </summary>
	[SerializeField]
	bool stickToY = false;

	/// <summary>
	/// Set my stickToY bool to newValue.
	/// </summary>
	/// <param name="newValue">What stickToY will be.</param>
	public void SetStickToY(bool newValue) {
		stickToY = newValue;
	}

	/// <summary>
	/// Handles the following and rotation with the target.
	/// </summary>
	private void FixedUpdate() {
		// The position I am moving towards.
		Vector3 newPos = target.position + target.forward * distanceForward;

		// If I am staying stuck to Y change it to my Y.
		if(stickToY) {
			newPos.y = transform.position.y;
		}

		// Move it to the newPos according to followSpeed.
		transform.position = Vector3.MoveTowards(transform.position, newPos, followSpeed);

		// And look at the target.
		transform.LookAt(transform.position * 2 - target.position);
	}
}
