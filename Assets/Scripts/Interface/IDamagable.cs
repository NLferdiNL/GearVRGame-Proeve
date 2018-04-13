using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable {

	int maxHealth {
		get;
	}

	int health {
		get;
	}

	void Damage(int value);

	void Heal(int value);
}
