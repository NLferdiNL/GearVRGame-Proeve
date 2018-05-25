﻿using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour, IDamagable {

	float _health = 100;

	[SerializeField]
	float _maxHealth = 100;

	float _dronesPerHitPoint = 0;

	int _totalDrones = 0;

	[SerializeField]
	Transform target;
	
	// TODO: Add proper methods for changing this.
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

	[SerializeField]
	List<Drone> _enemyBodies = new List<Drone>();

	void Start() {
		_health = _maxHealth;

		Drone[] drones = GetComponentsInChildren<Drone>();

		for(int i = 0; i < drones.Length; i++) {
			_enemyBodies.Add(drones[i]);
		}

		_totalDrones = _enemyBodies.Count;

		_dronesPerHitPoint = _health / _totalDrones;
	}

	public void Damage(float value) {
		// 66 health
		// 10 damage
		// 6 drones over
		// 1 drone to destroy

		_health -= value;

		int dronesToKeepAlive = Mathf.FloorToInt(_health / _dronesPerHitPoint);

		if(dronesToKeepAlive < _enemyBodies.Count) {
			for(int i = _enemyBodies.Count - 1; i > dronesToKeepAlive - 1; i--) {
				_enemyBodies[i].Kill();
				_enemyBodies.RemoveAt(i);
			}

			if(_enemyBodies.Count == 0) {
				SwarmContainer.Remove(transform);
				ScoreManager.EditScore(50);
				Destroy(gameObject);
			}
		}
	}

	public void Heal(float value) {
		// So that the laser doesn't have to check and just heals enemies.
		// Which harms them.
		Damage(value);
	}

	private void OnMouseDown() {
		Damage(MaxHealth);
	}
}
