using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

	[SerializeField]
	Transform target;

	[SerializeField]
	float followSpeed = 5;

	[SerializeField]
	float distanceForward = 100;

	private void FixedUpdate() {
		transform.position = Vector3.MoveTowards(transform.position, target.position + target.forward * distanceForward, followSpeed);
		transform.LookAt(transform.position * 2 - target.position);
	}
}
