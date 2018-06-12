using EnemyNav;
using UnityEngine;

/// <summary>
/// Used to make the swarms float around on a set Path.
/// </summary>
public class SwarmNavigation : MonoBehaviour {
	/// <summary>
	/// How fast am I going?
	/// </summary>
	[SerializeField]
	float moveSpeed = 10f;

	/// <summary>
	/// My parent component.
	/// </summary>
	[SerializeField]
	Swarm swarm;

	/// <summary>
	/// The dot above me. To make sure it is always facing up.
	/// </summary>
	[SerializeField]
	Transform radarDot;

	/// <summary>
	/// Minimum distance before moving to the next node.
	/// </summary>
	[SerializeField]
	float minimumDistanceToTarget = 2;

	/// <summary>
	/// How fast will I rotate towards my next target?
	/// </summary>
	[SerializeField]
	float rotationSpeed = 20;

	/// <summary>
	/// Current place in the Path.
	/// </summary>
	int currentIndexInPath = 1;

	/// <summary>
	/// A bool to be edited from outside to disable the
	/// navigation.
	/// </summary>
	public bool isAttacking {
		set {
			GetComponent<Rigidbody>().useGravity = value;
			enabled = !value;
		}

		get {
			return !enabled;
		}
	}

	/// <summary>
	/// Where am I going according to my Path object?
	/// </summary>
	Vector3 target {
		get {
			return path[currentIndexInPath];
		}
	}

	/// <summary>
	/// Holds my path.
	/// </summary>
	Path path;

	/// <summary>
	/// Sets the component up for use.
	/// </summary>
	private void Start() {
		// Get my parent component.
		swarm = GetComponent<Swarm>();

		// Do I have a path?
		// If I don't set path to a random one.
		if(path == null) {
			SetPath(Path.random);
		}

		// Teleport to my first node in the path.
		// This should never be visible to the user.
		transform.position = path[0];
	}

	/// <summary>
	/// Change my path.
	/// </summary>
	/// <param name="path">New Path</param>
	public void SetPath(Path path) {
		this.path = path;
	}

	/// <summary>
	/// Handles all the movement and the radardots rotation.
	/// </summary>
	private void FixedUpdate() {
			// Move to next node.
			MoveTo(target);

			// Make sure it is still facing up.
			radarDot.LookAt(radarDot.position + Vector3.up);
	}

	/// <summary>
	/// Happens every fixed update. To keep me moving to my target/current path node.
	/// </summary>
	/// <param name="target">Where am I going?</param>
	private void MoveTo(Vector3 target) {
		// Am I below or at minimum distance from my current target?
		// If yes add 1 to currentIndex.
		if(Vector3.Distance(target, transform.position) <= minimumDistanceToTarget) {
			currentIndexInPath++;
		}
		
		// Smoothly rotate to the next target.
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((target - transform.position)), Time.deltaTime * rotationSpeed);

		// And move forward.
		transform.position += transform.forward * Time.deltaTime * moveSpeed; //(target - transform.position).normalized * Time.deltaTime * moveSpeed;
	}
}