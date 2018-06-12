using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The parent of a set of drone bodies.
/// The main component for a swarm.
/// </summary>
public class Swarm : MonoBehaviour, IDamagable {

	/// <summary>
	/// My health.
	/// </summary>
	float _health = 100;

	/// <summary>
	/// My max health.
	/// </summary>
	[SerializeField]
	float _maxHealth = 100;

	/// <summary>
	/// Used to check if some drones need to be Killed off for losing health.
	/// For example:
	/// If I have 10 drones and 100 health.
	/// Losing 20 health will kill 20 drones.
	/// </summary>
	float _hitPointsPerDrone = 0;

	/// <summary>
	/// How many drones did I start with?
	/// Used in the killing off of drones.
	/// </summary>
	int _totalDrones = 0;

	/// <summary>
	/// My target, I'm holding it for all the child components.
	/// </summary>
	[SerializeField]
	Transform target;

	/// <summary>
	/// To trigger the SwarmDeathAnim.
	/// </summary>
	[SerializeField]
	Animator deathAnim;
	
	/// <summary>
	/// A public way to change my target.
	/// </summary>
	public Transform Target {
		get {
			return target;
		}
		set {
			target = value;
		}
	}

	/// <summary>
	/// A public way to get my health, implemented by IDamagable.
	/// </summary>
	public float Health {
		get {
			return _health;
		}
	}

	/// <summary>
	/// A public way to get my max health, implemented by IDamagable.
	/// </summary>
	public float MaxHealth {
		get {
			return _maxHealth;
		}
	}

	/// <summary>
	/// My drone bodies.
	/// </summary>
	[SerializeField]
	List<Drone> _enemyBodies = new List<Drone>();

	/// <summary>
	/// Set up all the variables and get all my children(drones).
	/// </summary>
	void Start() {
		// Set my health to max health.
		_health = _maxHealth;

		// Get all drones in my children.
		Drone[] drones = GetComponentsInChildren<Drone>();

		// Add them to the enemyBodies array.
		for(int i = 0; i < drones.Length; i++) {
			_enemyBodies.Add(drones[i]);
		}

		// Set the counter to the 100% amount of drones.
		_totalDrones = _enemyBodies.Count;

		// And calculate my health shared between the drones.
		_hitPointsPerDrone = _health / _totalDrones;
	}

	/// <summary>
	/// Damage me and take my drones down.
	/// Implemented by IDamagable.
	/// </summary>
	/// <param name="value">Amount to damage</param>
	public void Damage(float value) {
		// Remove the damage done.
		_health -= value;

		// Get the amount of drones to keep alive.
		int dronesToKeepAlive = Mathf.CeilToInt(_health / _hitPointsPerDrone);

		// If I have more drones than I need to keep alive.
		if(dronesToKeepAlive < _enemyBodies.Count) {
			// Kill the excess off.
			for(int i = _enemyBodies.Count - 1; i > dronesToKeepAlive - 1; i--) {
				_enemyBodies[i].Kill();
				_enemyBodies.RemoveAt(i);
			}

			// If I have no drones left, I died.
			if(_enemyBodies.Count == 0) {
				// Remove me from the container.
				SwarmContainer.Remove(transform);

				// Add score.
				ScoreManager.AddToScore(50);

				// Start my death anim.
				deathAnim.SetTrigger("OnDeath");
				
				// And destroy me.
				Destroy(gameObject, 3);
			}
		}
	}

	/// <summary>
	/// This is the same as Damage() because
	/// I don't need the swarms to be healed.
	/// Used by the laser to have all attacks under one name.
	/// 
	/// Implemented by IDamagable.
	/// </summary>
	/// <param name="value">Amount to damage</param>
	public void Heal(float value) {
		// There is no reason to heal them, so if the laser touches me,
		// healing me actually hurts me too.
		Damage(value);
	}
}
