using EnemyNav;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
				return pathNodes[index];
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

	[SerializeField]
	int enemyPerWaveIncrease = 2;

	int currentWave = 1;

	[SerializeField]
	Path[] paths = new Path[0];

	[SerializeField]
	float timeBetweenSpawns = 5f;

	[SerializeField]
	bool spawnEnemy = true;

	[SerializeField]
	bool endWhileLoop = false;

	[SerializeField]
	GameObject enemyPrefab;

	[SerializeField]
	int maxEnemies = 40;

	static List<Transform> enemies = new List<Transform>();

	public static Transform RandomEnemy {
		get {
            if (enemies.Count == 0)
                return null;

			return enemies[Random.Range(0, enemies.Count)];
		}
	}

    public static bool EnemiesAvailable
    {
        get
        {
            return enemies.Count > 0;
        }
    }

	public static void EnemyDied(Transform transform) {
		enemies.Remove(transform);
	}

	IEnumerator Start() {
		instance = this;

		while(!endWhileLoop) {
			yield return new WaitForSeconds(timeBetweenSpawns);
			if(spawnEnemy) {
				for(int i = 0; i < currentWave * enemyPerWaveIncrease; i++) {
					SpawnEnemy();
				}
				currentWave++;
			}
		}
	}

	void SpawnEnemy() {
		if(enemies.Count >= maxEnemies)
			return;

		GameObject enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
		enemyInstance.transform.LookAt(transform);

		enemies.Add(enemyInstance.transform);
	}
}
