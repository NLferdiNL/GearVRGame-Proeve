using System.Collections.Generic;

// Holds all the game setting flags.
// And allows access to check for them.
public class GameSettings {

	static List<string> settings = new List<string>();

	public static void Set(string value) {
		settings.Add(value);
	}

	public static void Remove(string value) {
		if(Has(value))
			settings.Remove(value);
	}

	public static bool Has(string value) {
		return settings.Contains(value);
	}
}
