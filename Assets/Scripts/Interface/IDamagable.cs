using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable {

	int MaxHealth {
		get;
	}

	int Health {
		get;
	}

	void Damage(int value);

	void Heal(int value);
}
