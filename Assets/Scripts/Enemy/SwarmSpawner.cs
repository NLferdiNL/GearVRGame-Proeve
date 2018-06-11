using EnemyNav;
using System.Collections;
using UnityEngine;

/// <summary>
/// This namespace holds the Path class used by the swarmnavigation to follow along a set path.
/// </summary>
namespace EnemyNav {
	using System;

	[Serializable]
	public class Path {
		/// <summary>
		/// If some object wants a random path, he can just call this.
		/// Returns null if no paths exist or the SwarmSpawner doesn't.
		/// Uses the swarmspawner as a container.
		/// </summary>
		public static Path random {
			get {
				if(!SwarmSpawner.Exists)
					return null;
				else if(SwarmSpawner.Paths.Length == 0)
					return null;

				return SwarmSpawner.Paths[UnityEngine.Random.Range(0, SwarmSpawner.Paths.Length)];
			}
		}
		/// <summary>
		/// My path nodes.
		/// </summary>
		public Vector3[] pathNodes;

		/// <summary>
		/// Get a node by index.
		/// </summary>
		/// <param name="index">index in path array</param>
		/// <returns></returns>
		public Vector3 this[int index] {
			get {
				if(index < pathNodes.Length)
					return pathNodes[index];

				return Vector3.zero;
			}
		}
		/// <summary>
		/// How long is my path, in nodes.
		/// </summary>
		public int Length {
			get {
				return pathNodes.Length;
			}
		}
	}
}

/// <summary>
/// Used to summon swarm.
/// Does nothing on its own.
/// </summary>
public class SwarmSpawner : MonoBehaviour {

	/// <summary>
	/// A reference to the Paths array.
	/// </summary>
	public static EnemyNav.Path[] Paths {
		get {
			return instance.paths;
		}
	}

	/// <summary>
	/// Does the spawner currently exist?
	/// </summary>
	public static bool Exists {
		get {
			return instance != null;
		}
	}

	/// <summary>
	/// A reference to the spawner.
	/// </summary>
	static SwarmSpawner instance;

#if UNITY_EDITOR
	/// <summary>
	/// The path that will be lit up to edit it.
	/// </summary>
	public int activePath = 0;
#endif
	
	/// <summary>
	/// Current wave, default to 1 to spawn 1 enemy.
	/// </summary>
	int currentWave = 1;

	/// <summary>
	/// A list of all available paths.
	/// </summary>
	[SerializeField]
    EnemyNav.Path[] paths = new EnemyNav.Path[0];

	/// <summary>
	/// The time between waves.
	/// Also used halved as extra time if there are still enemies left.
	/// </summary>
	[SerializeField]
	float timeBetweenSpawns = 5f;

	/// <summary>
	/// The array of enemies to instantiate from.
	/// </summary>
	[SerializeField]
	GameObject[] enemyPrefabs;

	/// <summary>
	/// A short way to get a random enemy from the enemyPrefabs.
	/// Returns null if something is wrong.
	/// </summary>
	GameObject randomEnemyPrefab {
		get {
            if (enemyPrefabs == null || enemyPrefabs.Length == 0)
                return null;

			return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
		}
	}

	/// <summary>
	/// The max amount of enemies allowed to exist.
	/// </summary>
	[SerializeField]
	int maxEnemies = 40;

	// Start() is used for debug purposes only.
	// The class shouldn't spawn itself.
	IEnumerator Start() {
		instance = this;

		// This loop should not exist in production.
		// This is ONLY for testing.
		while(true) {
			yield return new WaitForSeconds(timeBetweenSpawns);
			SpawnEnemies(3);
		}
	}

	/// <summary>
	/// Spawn a certain amount of enemies.
	/// </summary>
	/// <param name="amount">this many enemies</param>
	public static void SpawnEnemies(int amount) {
		for(int i = 0; i < amount; i++) {
			if(!instance.SpawnEnemy()) {
				break;
			}
		}
	}
	/// <summary>
	/// Spawn an enemy.
	/// Use SpawnEnemies to spawn mulitple.
	/// </summary>
	/// <returns>Returns false if something is wrong.</returns>
	public bool SpawnEnemy() {
		// Prevent going past the max.
		if(SwarmContainer.Count >= maxEnemies)
			return false;

		// Get a random enemy prefab reference.
        GameObject randomEnemyPrefab = this.randomEnemyPrefab;

		// If something went wrong getting the prefab.
        if (randomEnemyPrefab == null)
            return false;

		// Instantiate the prefab, place him on the right position in 0 rotation.
        GameObject enemyInstance = Instantiate(randomEnemyPrefab, transform.position, Quaternion.identity);

		// Look at my transform.
		enemyInstance.transform.LookAt(transform);

		// Add it to the container.
		SwarmContainer.Add(enemyInstance.transform);

		return true;
	}
#if UNITY_EDITOR
	/// <summary>
	/// To draw the paths on screen when the object is selected.
	/// </summary>
	private void OnDrawGizmosSelected() {
		// For all paths in the array.
		for(int i = 0; i < paths.Length; i++) {
			// Get the path in a variable for quick access.
            EnemyNav.Path path = paths[i];
			
			// Draw the starting point.
			Gizmos.DrawCube(path[0], new Vector3(2,2,2));

			// Draw the end point.
			Gizmos.DrawSphere(path[path.Length - 1], 2);

			// Draw a line between each path node.
			for(int j = 0; j < path.Length; j++) {
				if(j < path.Length - 1) {
					// If the path is the active path draw it in green else draw it in red.
					Debug.DrawLine(path[j], path[j + 1], i == activePath ? Color.green : Color.red, 0.01f);
				}
			}
		}
	}
#endif
}
