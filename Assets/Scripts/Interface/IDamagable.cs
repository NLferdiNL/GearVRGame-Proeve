/// <summary>
/// Used to implement health to any object.
/// One interface so any component can become
/// damageable by just adding this.
/// </summary>
public interface IDamagable {

	/// <summary>
	/// The max health getter, to give the public access to this objects max health.
	/// </summary>
	float MaxHealth {
		get;
	}

	/// <summary>
	/// The health getter, to give the publi access to this objects health.
	/// </summary>
	float Health {
		get;
	}

	/// <summary>
	/// To allow the outside to damage this object.
	/// </summary>
	/// <param name="value"></param>
	void Damage(float value);

	/// <summary>
	/// To allow the outside to heal this object.
	/// </summary>
	/// <param name="value"></param>
	void Heal(float value);
}
