using System.Collections.Generic;
/// <summary>
/// Holds all the game setting flags.
/// And allows access to check for them.
/// </summary>
public class GameSettings {

	/// <summary>
	/// The settings currently set.
	/// </summary>
	static List<string> settings = new List<string>();

	/// <summary>
	/// Add a setting
	/// </summary>
	/// <param name="value">name to add</param>
	public static void Set(string value) {
		settings.Add(value);
	}

	/// <summary>
	/// Remove a setting.
	/// </summary>
	/// <param name="value">name to remove</param>
	public static void Remove(string value) {
		if(Has(value))
			settings.Remove(value);
	}

	/// <summary>
	/// Do I have this setting?
	/// </summary>
	/// <param name="value">name to check</param>
	/// <returns>if name exists</returns>
	public static bool Has(string value) {
		return settings.Contains(value);
	}
}
