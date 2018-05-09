using EnemyNav;
using UnityEngine;

public class SwarmNavigation : MonoBehaviour {
	[SerializeField]
	Vector3 currentPathTarget;

	[SerializeField]
	float moveSpeed = 10f;

	[SerializeField]
	Swarm swarm;

	int currentIndexInPath = 1;

	Vector3 target {
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

		transform.position = path[0];
	}

	public void SetPath(Path path) {
		this.path = path;
	}

	private void FixedUpdate() {
		MoveTo(target, 2);
	}

	private bool CheckDirectLine() {
		return !Physics.Linecast(transform.position, target);
		//return !Physics.CapsuleCast(transform.position, target.position, transform.lossyScale.magnitude, (target.position - transform.position).normalized);
	}

	private void MoveTo(Vector3 target, float minimumDistanceToTarget) {
		if(Vector3.Distance(target, transform.position) < minimumDistanceToTarget) {
			currentIndexInPath++;
		}
		
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((target - transform.position)), Time.deltaTime * rotationSpeed);
		transform.position += transform.forward * Time.deltaTime * moveSpeed; //(target - transform.position).normalized * Time.deltaTime * moveSpeed;
		transform.LookAt(target, Vector3.up);
	}
}