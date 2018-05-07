using EnemyNav;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyNav {
	using UnityEngine.SceneManagement;

	public enum NavigationStateEnum {
		AttackingTarget = 0,
		ToNode = 1,
		UnknownPath = 2,
		NoTarget = 3,
		ToTarget = 4
	}

	[CreateAssetMenu]
	public class Path : ScriptableObject {
		private static Path[] allPaths = null;

		public static Path[] AllPaths {
			get {
				if(allPaths == null) {
					allPaths = Resources.LoadAll<Path>("Resources/Paths/" + SceneManager.GetActiveScene().name + "/");
				}

				return allPaths;
			}
		}

		public static Path random {
			get {
				return AllPaths[UnityEngine.Random.Range(0, AllPaths.Length - 1)];
			}
		}
		public Transform[] pathNodes;

		public Transform this[int index] {
			get {
				return pathNodes[index];
			}
		}
	}
}

public class SwarmNavigation : MonoBehaviour {
	bool nearCurrentTarget = true;

	[SerializeField]
	Vector3 currentPathTarget;

	[SerializeField]
	float moveSpeed = 10f;

	[SerializeField]
	Swarm swarm;

	[SerializeField]
	NavigationStateEnum navigationState;

	public NavigationStateEnum NavigationState {
		get {
			return navigationState;
		}

		set {
			navigationState = value;
		}
	}

	int currentIndexInPath = 0;

	Transform target {
		get {
			return path[currentIndexInPath];
		}
	}

	Path path;

	private void Start() {
		swarm = GetComponent<Swarm>();

		if(path == null) {
			SetPath(Path.random);
		}
	}

	public void SetPath(Path path) {
		this.path = path;
	}

	private void FixedUpdate() {
		if(NavigationState == NavigationStateEnum.AttackingTarget)
			return;

		if(nearCurrentTarget) {
			if(NavigationState == NavigationStateEnum.ToNode) {
				if(Vector3.Distance(transform.position, currentPathTarget) <= 3) {
					NavigationState = NavigationStateEnum.UnknownPath;
				}
			} else if(NavigationState == NavigationStateEnum.NoTarget) {
				NavigationState = target != null ? NavigationStateEnum.UnknownPath : NavigationStateEnum.NoTarget;
			}

			if(NavigationState == NavigationStateEnum.UnknownPath) {
				if(target != null) {
					if(CheckDirectLine()) {
						NavigationState = NavigationStateEnum.ToTarget;
					} else {
						NavigationState = NavigationStateEnum.ToNode;
						currentPathTarget = NavMesh.GetNearestNodePos(target.position, transform.position);
					}
				} else {
					NavigationState = NavigationStateEnum.NoTarget;
				}
			}
		}

		switch(NavigationState) {
			case NavigationStateEnum.ToNode:
				MoveTo(currentPathTarget, 0);
				break;
			case NavigationStateEnum.ToTarget:
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