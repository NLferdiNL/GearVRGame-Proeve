using UnityEngine;

/// <summary>
/// Follow the targets rotation without following position.
/// </summary>
public class RotateWithTarget : MonoBehaviour {
	/// <summary>
	/// Whos rotation am I following?
	/// </summary>
	[SerializeField]
	Transform target;

	/// <summary>
	/// How fast will I follow?
	/// </summary>
	[SerializeField]
	float rotateSpeed = 5;

	/// <summary>
	/// Make the rotation happen.
	/// </summary>
	void FixedUpdate () {
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, target.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), Time.deltaTime * rotateSpeed);
	}
}
