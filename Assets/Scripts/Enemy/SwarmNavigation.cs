using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmNavigation : MonoBehaviour {

	public enum NavigationState {
		NoTarget = 0,
		ToNode = 1,
		ToTarget = 2,
		UnknownPath = 3
	}

	bool nearCurrentTarget = true;

	[SerializeField]
	Transform target;

	[SerializeField]
	Vector3 node;

	[SerializeField]
	float moveSpeed = 10f;

	[SerializeField]
	NavigationState navigationState = NavigationState.NoTarget;

	private void FixedUpdate() {
		if(nearCurrentTarget) {
			if(navigationState == NavigationState.ToNode) {
				if(Vector3.Distance(transform.position, node) <= 3) {
					navigationState = NavigationState.UnknownPath;
				}
			} else if(navigationState == NavigationState.NoTarget) {
				navigationState = target != null ? NavigationState.UnknownPath : NavigationState.NoTarget;
			}

			if(navigationState == NavigationState.UnknownPath) {
				if(target != null) {
					if(CheckDirectLine()) {
						navigationState = NavigationState.ToTarget;
					} else {
						navigationState = NavigationState.ToNode;
						node = NavMesh.GetNearestNodePos(target.position, transform.position);
					}
				} else {
					navigationState = NavigationState.NoTarget;
				}
			}
		}

		switch(navigationState) {
			case NavigationState.ToNode:
				MoveTo(node, 0);
				break;
			case NavigationState.ToTarget:
				MoveTo(target.position, 5);
				break;
		}
	}

	private bool CheckDirectLine() {
		return !Physics.Linecast(transform.position, target.position);
		//return !Physics.CapsuleCast(transform.position, target.position, transform.lossyScale.magnitude, (target.position - transform.position).normalized);
	}

	private void MoveTo(Vector3 target, float minimumDistanceToTarget) {
		if(Vector3.Distance(target, transform.position) < minimumDistanceToTarget) {
			nearCurrentTarget = true;
			return;
		}

		transform.position += (target - transform.position).normalized * Time.deltaTime * moveSpeed;
		transform.LookAt(target, Vector3.up);
	}
}
