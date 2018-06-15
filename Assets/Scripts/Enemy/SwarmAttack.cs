using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all the damage dealing,
/// this waits until SwarmNavigation brings it
/// close enough and takes over.
/// </summary>
public class SwarmAttack : MonoBehaviour {

	/// <summary>
	/// My parent component.
	/// </summary>
	[SerializeField]
	Swarm swarm;

	/// <summary>
	/// The component to take over when near a target.
	/// </summary>
	SwarmNavigation swarmNavigation;

	/// <summary>
	/// To shorten the use of target.
	/// Allows getting and setting.
	/// </summary>
	Transform target {
		get {
			return swarm.Target;
		}

		set {
			swarm.Target = value;
		}
	}

	/// <summary>
	/// How close until I can attack.
	/// </summary>
	[SerializeField]
	float attackRange = 5;

	/// <summary>
	/// How much damage I do each frame?
	/// </summary>
	[SerializeField]
	float damagePerFrame = 1;
	
	/// <summary>
	/// Set up the component for use.
	/// </summary>
	void Start () {
		swarm = GetComponent<Swarm>();
		swarmNavigation = GetComponent<SwarmNavigation>();
	}

	/// <summary>
	/// Something is near me. Check if it is a building so I can attack it.
	/// </summary>
	/// <param name="other">The other collider</param>
	private void OnTriggerEnter(Collider other) {
		// Can I attack this collider?
		if(other.gameObject.GetComponentInChildren<Building>() != null && other.gameObject.layer != LayerMask.NameToLayer("Ground")) {
			// Start attacking.
			target = other.transform;

			swarmNavigation.isAttacking = true;
		}
	}

	/// <summary>
	/// Checks the distance between the target and attack range.
	/// If it is within range, take control from the navigation and start attacking.
	/// </summary>
	private void FixedUpdate() {
		if(target == null)
			return;

		if(Vector3.Distance(target.position, transform.position) <= attackRange) {
			target.SendMessage("Damage", damagePerFrame, SendMessageOptions.DontRequireReceiver);
			if(swarmNavigation.isAttacking)
				swarmNavigation.enabled = false;
		}
	}
}
