using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all the enemies in the game.
/// Doesn't need to be placed in game.
/// </summary>
public class SwarmContainer {

	/// <summary>
	/// A container for all enemies in existance.
	/// </summary>
	static List<Transform> enemies = new List<Transform>();
	
	/// <summary>
	/// If a component needs a random enemy.
	/// Returns null if no enemies exist.
	/// </summary>
	public static Transform RandomEnemy {
		get {
			if(!EnemiesAvailable)
				return null;

			return enemies[UnityEngine.Random.Range(0, enemies.Count)];
		}
	}

	/// <summary>
	/// Are there any enemies?
	/// </summary>
	public static bool EnemiesAvailable {
		get {
			return Count > 0;
		}
	}

	/// <summary>
	/// How many enemies exist?
	/// </summary>
	public static int Count {
		get {
			return enemies.Count;
		}
	}

	/// <summary>
	/// Add an enemy to the array.
	/// </summary>
	/// <param name="enemy"></param>
	public static void Add(Transform enemy) {
		if(enemies.IndexOf(enemy) == -1)
			enemies.Add(enemy);
	}

	/// <summary>
	/// Remove an enemy from the array.
	/// </summary>
	/// <param name="enemy"></param>
	public static void Remove(Transform enemy) {
		// Does this enemy exist in the array?
		if(enemies.IndexOf(enemy) != -1)
			// Remove it.
			enemies.Remove(enemy);
	}

	/// <summary>
	/// Get an enemy by index.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public static Transform GetEnemy(int index) {
		// Is the index in range?
		if(index > 0 && index < Count) {
			// Return the enemy found at the index.
			return enemies[index];
		} else {
			// It wasn't in range, return null.
			return null;
		}
	}

	/// <summary>
	/// Call this to make sure no references are left behind.
	/// Useful to clean up a scene too.
	/// </summary>
	public static void Cleanup() {
		// Loop through all enemies.
		for(int i = Count; i > -1; i++) {
			// Store a reference to their transform.
			Transform enemy = enemies[i];

			// Remove the enemy from the array.
			Remove(enemy);

			// If the enemy exists destroy it.
			if(enemy != null)
				Object.Destroy(enemy.gameObject);
		}
	}
}
