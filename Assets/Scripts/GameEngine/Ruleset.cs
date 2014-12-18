using UnityEngine;
using System.Collections.Generic;

public class Ruleset : List<Rule> {

	public bool isValid(GameState state) {
		foreach (Rule r in this) {
			if (!r.applyTo(state).isValid()) {
				return false;
			}
		}
		return true;
	}
	
	public GameResult apply(GameState state) {
		return null;
	}
	
}