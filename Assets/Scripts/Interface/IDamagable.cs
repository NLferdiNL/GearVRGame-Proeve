public interface IDamagable {

	float MaxHealth {
		get;
	}

	float Health {
		get;
	}

	void Damage(float value);

	void Heal(float value);
}
