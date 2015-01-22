using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class StateRenderer {
	
    public Dictionary<WorldObject, GameObject> rendered = new Dictionary<WorldObject, GameObject>();
    public Dictionary<WorldObject, string> prefabs = new Dictionary<WorldObject, string>();

	public StateRenderer() {
	}

    public abstract void render(GameEngine engine);
	
}