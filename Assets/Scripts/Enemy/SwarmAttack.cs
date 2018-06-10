using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all the damage dealing,
/// this waits until SwarmNavigation brings it
/// close enough and takes over.
/// </summary>
public class SwarmAttack : MonoBehaviour {

	// My owner.
	[SerializeField]
	Swarm swarm;

	// To take over when near.
	SwarmNavigation swarmNavigation;

	// To shorten the use of target.
	Transform target {
		get {
			return swarm.Target;
		}

		set {
			swarm.Target = value;
		}
	}

	// How close until I can attack.
	[SerializeField]
	float attackRange = 5;

	[SerializeField]
	float damagePerFrame = 1;
	
	void Start () {
		swarm = GetComponent<Swarm>();
		swarmNavigation = GetComponent<SwarmNavigation>();
	}

	private void OnTriggerEnter(Collider other) {
		// Can I attack this collider?
		if(other.gameObject.GetComponentInChildren<Building>() != null && other.gameObject.layer != LayerMask.NameToLayer("Ground")) {
			// Start attacking.
			target = other.transform;
		}
	}

	private void FixedUpdate() {
		if(target == null)
			return;

		if(Vector3.Distance(target.position, transform.position) <= attackRange) {
			target.SendMessage("Damage", damagePerFrame, SendMessageOptions.DontRequireReceiver);
			if(swarmNavigation.enabled)
				swarmNavigation.enabled = false;
		}
	}
}
