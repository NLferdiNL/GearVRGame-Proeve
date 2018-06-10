using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The parent of a set of drone bodies.
/// The main component for a swarm.
/// </summary>
public class Swarm : MonoBehaviour, IDamagable {

	float _health = 100;

	[SerializeField]
	float _maxHealth = 100;

	// Used to check if some drones need to be Killed off for losing health.
	// For example:
	// If I have 10 drones and 100 health.
	// Losing 20 health will kill 20 drones.
	float _hitPointsPerDrone = 0;

	// How many drones did I start with?
	// Used in the killing off of drones.
	int _totalDrones = 0;

	// My target, I'm holding it for all the child components.
	[SerializeField]
	Transform target;
	
	public Transform Target {
		get {
			return target;
		}
		set {
			target = value;
		}
	}

	public float Health {
		get {
			return _health;
		}
	}

	public float MaxHealth {
		get {
			return _maxHealth;
		}
	}

	// My drone bodies.
	[SerializeField]
	List<Drone> _enemyBodies = new List<Drone>();

	void Start() {
		_health = _maxHealth;

		Drone[] drones = GetComponentsInChildren<Drone>();

		for(int i = 0; i < drones.Length; i++) {
			_enemyBodies.Add(drones[i]);
		}

		_totalDrones = _enemyBodies.Count;

		_hitPointsPerDrone = _health / _totalDrones;
	}

	public void Damage(float value) {
		_health -= value;

		int dronesToKeepAlive = Mathf.FloorToInt(_health / _hitPointsPerDrone);

		if(dronesToKeepAlive < _enemyBodies.Count) {
			for(int i = _enemyBodies.Count - 1; i > dronesToKeepAlive - 1; i--) {
				_enemyBodies[i].Kill();
				_enemyBodies.RemoveAt(i);
			}

			if(_enemyBodies.Count == 0) {
				SwarmContainer.Remove(transform);
				ScoreManager.AddToScore(50);
				Destroy(gameObject);
			}
		}
	}

	public void Heal(float value) {
		// There is no reason to heal them, so if the laser touches me,
		// healing me actually hurts me too.
		Damage(value);
	}
}
