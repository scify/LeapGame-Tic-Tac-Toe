using UnityEngine;
using System.Collections.Generic;

public abstract class Ruleset<T> : List<T> where T : Rule {
    
    public abstract void applyTo(GameState state, GameEvent eve, GameEngine engine);
	
}