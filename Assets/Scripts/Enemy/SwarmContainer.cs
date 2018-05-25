using System;
using System.Collections.Generic;
using UnityEngine;

public class SwarmContainer : MonoBehaviour {

	static List<Transform> enemies = new List<Transform>();
	
	public static Transform RandomEnemy {
		get {
			if(enemies.Count == 0)
				return null;

			return enemies[UnityEngine.Random.Range(0, enemies.Count)];
		}
	}

	public static bool EnemiesAvailable {
		get {
			return enemies.Count > 0;
		}
	}

	public static int Count {
		get {
			return enemies.Count;
		}
	}

	public static void Add(Transform enemy) {
		if(enemies.IndexOf(enemy) == -1)
			enemies.Add(enemy);
	}

	public static void Remove(Transform enemy) {
		if(enemies.IndexOf(enemy) != -1)
			enemies.Remove(enemy);
	}

	public static Transform GetEnemy(int index) {
		if(index > 0 && index < Count) {
			return enemies[index];
		} else {
			throw new IndexOutOfRangeException();
		}
	}
}
