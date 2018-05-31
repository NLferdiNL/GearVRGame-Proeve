using EnemyNav;
using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace EnemyNav {
	using System;

	[Serializable]
	public class Path {
		public static Path random {
			get {
				return SwarmSpawner.Paths[UnityEngine.Random.Range(0, SwarmSpawner.Paths.Length)];
			}
		}

		public Vector3[] pathNodes;

		public Vector3 this[int index] {
			get {
				if(index < pathNodes.Length)
					return pathNodes[index];

				return Vector3.zero;
			}
		}

		public int Length {
			get {
				return pathNodes.Length;
			}
		}
	}
}

public class SwarmSpawner : MonoBehaviour {

	public static Path[] Paths {
		get {
			return instance.paths;
		}
	}

	static SwarmSpawner instance;

#if UNITY_EDITOR
	public int activePath = 0;
#endif

	[SerializeField]
	int enemyPerWaveIncrease = 2;

	int currentWave = 1;

	[SerializeField]
	Path[] paths = new Path[0];

	[SerializeField]
	float timeBetweenSpawns = 5f;

	[SerializeField]
	GameObject[] enemyPrefabs;

	GameObject randomEnemyPrefab {
		get {
			return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
		}
	}

	[SerializeField]
	int maxEnemies = 40;

	IEnumerator Start() {
		instance = this;

		while(true) {
			yield return new WaitForSeconds(10);
			SpawnSwarms(3);
		}
	}

	public static void SpawnSwarms(int amount) {
		for(int i = 0; i < amount; i++) {
			instance.SpawnEnemy();
		}
	}

	void SpawnEnemy() {
		if(SwarmContainer.Count >= maxEnemies)
			return;

		GameObject enemyInstance = Instantiate(randomEnemyPrefab, transform.position, Quaternion.identity);
		enemyInstance.transform.LookAt(transform);

		SwarmContainer.Add(enemyInstance.transform);
	}
#if UNITY_EDITOR
	private void OnDrawGizmosSelected() {
		for(int i = 0; i < paths.Length; i++) {
			Path path = paths[i];
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
