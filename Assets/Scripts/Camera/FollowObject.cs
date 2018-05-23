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

	[SerializeField]
	bool stickToY = false;

	private void FixedUpdate() {
		Vector3 newPos = target.position + target.forward * distanceForward;

		if(stickToY) {
			newPos.y = transform.position.y;
		}

		transform.position = Vector3.MoveTowards(transform.position, newPos, followSpeed);
		transform.LookAt(transform.position * 2 - target.position);
	}
}
