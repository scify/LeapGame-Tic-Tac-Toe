using UnityEngine;
using System.Collections;

public abstract class Rule {

    public string category;

    public Rule(string category) {
        this.category = category;
    }

	public abstract bool applyTo(GameState state, GameEvent eve, GameEngine engine);
	
}