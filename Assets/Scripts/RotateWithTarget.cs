using UnityEngine;

/// <summary>
/// Follow the targets rotation without following position.
/// </summary>
public class RotateWithTarget : MonoBehaviour {

	[SerializeField]
	Transform target;

	[SerializeField]
	float rotateSpeed = 5;
	void FixedUpdate () {
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, target.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), Time.deltaTime * rotateSpeed);
	}
}
