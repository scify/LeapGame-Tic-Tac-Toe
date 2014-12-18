using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class StateRenderer {
	
    public Dictionary<WorldObject, GameObject> rendered = new Dictionary<WorldObject, GameObject>();

	public StateRenderer() {
	}

    public abstract void render(GameState state);
	
}