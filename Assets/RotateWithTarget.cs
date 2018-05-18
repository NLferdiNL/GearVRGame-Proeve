using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithTarget : MonoBehaviour {

	[SerializeField]
	Transform target;

	void FixedUpdate () {
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, target.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
	}
}
