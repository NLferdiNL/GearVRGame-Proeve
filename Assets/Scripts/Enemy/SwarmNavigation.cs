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
	Vector3 currentPathTarget;

	[SerializeField]
	float moveSpeed = 10f;

	[SerializeField]
	NavigationState navigationState = NavigationState.ToNode;

	public void SetPathTarget(Vector3 spawnPathTarget, bool setNavigationState = true) {
		currentPathTarget = spawnPathTarget;
		if(setNavigationState)
			navigationState = NavigationState.ToNode;
	}

	public void SetTarget(Transform transform, bool setNavigationState = true) {
		target = transform;
		if(setNavigationState)
			navigationState = NavigationState.UnknownPath;
	}

	private void FixedUpdate() {
		if(nearCurrentTarget) {
			if(navigationState == NavigationState.ToNode) {
				if(Vector3.Distance(transform.position, currentPathTarget) <= 3) {
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
						currentPathTarget = NavMesh.GetNearestNodePos(target.position, transform.position);
					}
				} else {
					navigationState = NavigationState.NoTarget;
				}
			}
		}

		switch(navigationState) {
			case NavigationState.ToNode:
				MoveTo(currentPathTarget, 0);
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
		
		//Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((target - transform.position)), Time.deltaTime);
		transform.position += (target - transform.position).normalized * Time.deltaTime * moveSpeed;
		transform.LookAt(target, Vector3.up);
	}
}
