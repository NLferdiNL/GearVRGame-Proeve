/// <summary>
/// Used to implement health to any object.
/// One interface so any component can become
/// damageable by just adding this.
/// </summary>
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
