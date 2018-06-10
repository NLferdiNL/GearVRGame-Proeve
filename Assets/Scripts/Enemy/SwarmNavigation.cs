using EnemyNav;
using UnityEngine;

/// <summary>
/// Used to make the swarms float around on a set Path.
/// </summary>
public class SwarmNavigation : MonoBehaviour {

	// How fast am I going?
	[SerializeField]
	float moveSpeed = 10f;

	// My parent component.
	[SerializeField]
	Swarm swarm;

	// The dot above me. To make sure it is always facing up.
	[SerializeField]
	Transform radarDot;

	// Current place in the Path.
	int currentIndexInPath = 1;

	// Where am I going according to my Path object?
	Vector3 target {
		get {
			return path[currentIndexInPath];
		}
	}

	// Holds my path.
    Path path;

	private void Start() {
		swarm = GetComponent<Swarm>();

		if(path == null) {
			SetPath(Path.random);
		}

		transform.position = path[0];
	}

	public void SetPath(Path path) {
		this.path = path;
	}

	private void FixedUpdate() {
		MoveTo(target, 2);
		radarDot.LookAt(radarDot.position + Vector3.up);
	}
	
	// Happens every fixed update. To keep me moving to my target/current path node.
	private void MoveTo(Vector3 target, float minimumDistanceToTarget) {
		if(Vector3.Distance(target, transform.position) < minimumDistanceToTarget) {
			currentIndexInPath++;
		}
		
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((target - transform.position)), Time.deltaTime * rotationSpeed);
		transform.position += transform.forward * Time.deltaTime * moveSpeed; //(target - transform.position).normalized * Time.deltaTime * moveSpeed;
		transform.LookAt(target, Vector3.up);
	}
}