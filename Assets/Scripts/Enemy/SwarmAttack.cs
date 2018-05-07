﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmAttack : MonoBehaviour {

	[SerializeField]
	Swarm swarm;

	SwarmNavigation swarmNavigation;

	Transform target {
		get {
			return swarm.Target;
		}

		set {
			swarm.Target = value;
		}
	}

	[SerializeField]
	float attackRange = 5;

	[SerializeField]
	float damagePerFrame = 1;

	List<Collider> withinRange = new List<Collider>();

	void Start () {
		swarm = GetComponent<Swarm>();
		swarmNavigation = GetComponent<SwarmNavigation>();
	}

	private void OnTriggerEnter(Collider other) {
		withinRange.Add(other);

		if(other.gameObject.GetComponentInChildren<Building>() != null && other.gameObject.layer != LayerMask.NameToLayer("Ground")) {
			target = other.transform;
		}
	}

	private void OnTriggerExit(Collider other) {
		if(withinRange.Contains(other))
			withinRange.Remove(other);
	}

	private void FixedUpdate() {
		if(Vector3.Distance(target.position, transform.position) <= attackRange) {
			target.SendMessage("Damage", damagePerFrame, SendMessageOptions.DontRequireReceiver);
			if(swarmNavigation.NavigationState1 != SwarmNavigation.NavigationState.AttackingTarget)
				swarmNavigation.NavigationState1 = SwarmNavigation.NavigationState.AttackingTarget;
		} else {
			if(swarmNavigation.NavigationState1 == SwarmNavigation.NavigationState.AttackingTarget)
				swarmNavigation.NavigationState1 = SwarmNavigation.NavigationState.UnknownPath;
		}
	}
}
