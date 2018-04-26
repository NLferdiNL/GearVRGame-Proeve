using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMesh : MonoBehaviour {

	[SerializeField]
	GameObject[] nodes;

	private static NavMesh instance;

	private void Start() {
		nodes = GameObject.FindGameObjectsWithTag("NavNode");
		instance = this;
	}

	public static Vector3 GetNearestNodePos(Vector3 target, Vector3 agent) {
		Vector3 nodePosToReturn = Vector3.zero;

		float distanceToTarget, distanceToAgent;
		distanceToTarget = distanceToAgent = 1000000;

		for(int i = 0; i < instance.nodes.Length; i++) {
			Transform node = instance.nodes[i].transform;
			float currNodeDistanceToTarget = Vector3.Distance(target, node.position);
			float currNodeDistanceToAgent = Vector3.Distance(agent, node.position);

			if(currNodeDistanceToAgent < distanceToAgent && currNodeDistanceToTarget < distanceToTarget) {
				if(!Physics.Linecast(agent, node.position)) {
					distanceToTarget = currNodeDistanceToTarget;
					distanceToAgent = currNodeDistanceToAgent;
					nodePosToReturn = node.position;
				}
			}
		}

		return nodePosToReturn;
	}
}
