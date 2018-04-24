using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSpawner : MonoBehaviour {

	[SerializeField]
	float radius = 10;

	[SerializeField]
	float distanceUpBeforeTarget = 15;

	[SerializeField]
	float timeBetweenSpawns = 5f;

	[SerializeField]
	bool spawnEnemy = true;

	[SerializeField]
	bool endWhileLoop = false;

	[SerializeField]
	GameObject enemyPrefab;

	[SerializeField]
	Transform defaultTarget;

	[SerializeField]
	int maxEnemies = 10;

	//DEFINITLY REPLACE THIS FOR A BETTER METHOD
	public static int currentEnemyCount = 0;

	IEnumerator Start() {
		while(!endWhileLoop) {
			yield return new WaitForSeconds(timeBetweenSpawns);
			if(spawnEnemy && currentEnemyCount < maxEnemies);
				SpawnEnemy();
		}
	}

	void SpawnEnemy() {
		currentEnemyCount++;
		GameObject enemyInstance = Instantiate(enemyPrefab, transform.forward * radius, Quaternion.identity);
		enemyInstance.transform.LookAt(transform);

		SwarmNavigation enemyInstanceNav = enemyInstance.GetComponent<SwarmNavigation>();
		enemyInstanceNav.SetPathTarget(enemyInstance.transform.position + enemyInstance.transform.up * distanceUpBeforeTarget);
		enemyInstanceNav.SetTarget(defaultTarget, false);
	}
}
