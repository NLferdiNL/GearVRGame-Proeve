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

		// My nodes.
		public Vector3[] pathNodes;

		/// <summary>
		/// Get a node by index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Vector3 this[int index] {
			get {
				if(index < pathNodes.Length)
					return pathNodes[index];

				return Vector3.zero;
			}
		}

		// How long is my path, in nodes.
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

	public static EnemyNav.Path[] Paths {
		get {
			return instance.paths;
		}
	}

	public static bool Exists {
		get {
			return instance != null;
		}
	}

	public static SwarmSpawner instance;

#if UNITY_EDITOR
	public int activePath = 0;
#endif

	[SerializeField]
	int enemyPerWaveIncrease = 2;

	int currentWave = 1;

	[SerializeField]
    EnemyNav.Path[] paths = new EnemyNav.Path[0];

	[SerializeField]
	float timeBetweenSpawns = 5f;

	[SerializeField]
	GameObject[] enemyPrefabs;

	GameObject randomEnemyPrefab {
		get {
            if (enemyPrefabs == null || enemyPrefabs.Length == 0)
                return null;

			return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
		}
	}

	[SerializeField]
	int maxEnemies = 40;
    
    /// <summary>
    /// Sets up a public instance for spawning
    /// enemies without reference to this component.
    /// </summary>
	void Start() {
		instance = this;
	}

	public static void SpawnSwarms(int amount) {
		for(int i = 0; i < amount; i++) {
			instance.SpawnEnemy();
		}
	}

	public void SpawnEnemy() {
		if(SwarmContainer.Count >= maxEnemies)
			return;

        GameObject randomEnemyPrefab = this.randomEnemyPrefab;

        if (randomEnemyPrefab == null)
            return;

        GameObject enemyInstance = Instantiate(randomEnemyPrefab, transform.position, Quaternion.identity);
		enemyInstance.transform.LookAt(transform);

		SwarmContainer.Add(enemyInstance.transform);
	}
#if UNITY_EDITOR
	// To draw the paths on screen when the object is selected.
	private void OnDrawGizmosSelected() {
		for(int i = 0; i < paths.Length; i++) {
            EnemyNav.Path path = paths[i];
			Gizmos.DrawCube(path[0], new Vector3(2,2,2));
			Gizmos.DrawSphere(path[path.Length - 1], 2);
			for(int j = 0; j < path.Length; j++) {
				if(j < path.Length - 1) {
					Debug.DrawLine(path[j], path[j + 1], i == activePath ? Color.green : Color.red, 0.01f);
				}
			}
		}
	}
#endif
}
