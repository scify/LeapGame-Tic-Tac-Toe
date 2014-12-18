using UnityEngine;
using System.Collections;

public abstract class Rule {
	
	public Rule() {
	}
	
	public abstract GameResult applyTo(GameState state);
	
}