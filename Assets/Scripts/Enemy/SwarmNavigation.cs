using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmNavigation : MonoBehaviour {

	public enum NavigationState {
		NoTarget = 0,
		ToNode = 1,
		ToTarget = 2,
		AttackingTarget = 3,
		UnknownPath = 4
	}

	bool nearCurrentTarget = true;
	
	Transform target {
		get {
			return swarm.Target;
		}

		set {
			swarm.Target = value;
		}
	}

	public NavigationState NavigationState1 {
		get {
			return navigationState;
		}

		set {
			navigationState = value;
		}
	}

	[SerializeField]
	Vector3 currentPathTarget;

	[SerializeField]
	float moveSpeed = 10f;

	[SerializeField]
	float rotationSpeed = 20f;

	[SerializeField]
	NavigationState navigationState = NavigationState.ToNode;

	[SerializeField]
	Swarm swarm;

	private void Start() {
		swarm = GetComponent<Swarm>();
	}

	// TODO: Move these to Swarm and add UnityEvents
	public void SetPathTarget(Vector3 spawnPathTarget, bool setNavigationState = true) {
		currentPathTarget = spawnPathTarget;
		if(setNavigationState)
			NavigationState1 = NavigationState.ToNode;
	}

	public void SetTarget(Transform transform, bool setNavigationState = true) {
		target = transform;
		if(setNavigationState)
			NavigationState1 = NavigationState.UnknownPath;
	}
	// END TODO

	private void FixedUpdate() {
		if(NavigationState1 == NavigationState.AttackingTarget)
			return;

		if(nearCurrentTarget) {
			if(NavigationState1 == NavigationState.ToNode) {
				if(Vector3.Distance(transform.position, currentPathTarget) <= 3) {
					NavigationState1 = NavigationState.UnknownPath;
				}
			} else if(NavigationState1 == NavigationState.NoTarget) {
				NavigationState1 = target != null ? NavigationState.UnknownPath : NavigationState.NoTarget;
			}

			if(NavigationState1 == NavigationState.UnknownPath) {
				if(target != null) {
					if(CheckDirectLine()) {
						NavigationState1 = NavigationState.ToTarget;
					} else {
						NavigationState1 = NavigationState.ToNode;
						currentPathTarget = NavMesh.GetNearestNodePos(target.position, transform.position);
					}
				} else {
					NavigationState1 = NavigationState.NoTarget;
				}
			}
		}

		switch(NavigationState1) {
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
		
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((target - transform.position)), Time.deltaTime * rotationSpeed);
		transform.position += transform.forward * Time.deltaTime * moveSpeed; //(target - transform.position).normalized * Time.deltaTime * moveSpeed;
		transform.LookAt(target, Vector3.up);
	}
}
