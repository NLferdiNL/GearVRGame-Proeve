using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMesh : MonoBehaviour {

	[SerializeField]
	GameObject[] nodes;

	private static NavMesh instance;

	//[SerializeField]
	//private Node[] nodeMap;

	public class Node {
		public class ConnectedNode {
			public Transform otherNode { get; private set; }
			public float distance { get; private set; }

			public ConnectedNode(Transform otherNode, float distance) {

			}
		}

		public List<ConnectedNode> connectedNodes = new List<ConnectedNode>();
	}

	private void Start() {
		nodes = GameObject.FindGameObjectsWithTag("NavNode");
		//GenerateMap(nodes);
		instance = this;
	}
	
	// Make a map of all nodes.
	// Is not used yet.
	private void GenerateMap(GameObject[] nodes) {
		for(int i = 0; i < nodes.Length; i++) {
			GameObject nodeA = nodes[i];
			List<Node.ConnectedNode> connectedNodes = new List<Node.ConnectedNode>();

			for(int j = 0; j < nodes.Length; j++) {
				GameObject nodeB = nodes[j];

				if(!Physics.Linecast(nodeA.transform.position, nodeB.transform.position)) {
					Node.ConnectedNode connectedNode = new Node.ConnectedNode(nodeB.transform, 
																			  Vector3.Distance(nodeA.transform.position, 
																							   nodeB.transform.position));
					connectedNodes.Add(connectedNode);
					Debug.DrawLine(nodeA.transform.position, nodeB.transform.position, Color.red, Mathf.Infinity);
				}
			}
		}
	}

	/// <summary>
	/// Checks all nodes for closest to target and player.
	/// </summary>
	/// <param name="target"></param>
	/// <param name="agent"></param>
	/// <returns></returns>
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
