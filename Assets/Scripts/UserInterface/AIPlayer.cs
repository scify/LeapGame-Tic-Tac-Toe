using UnityEngine;
using System.Collections;

public abstract class AIPlayer : Player {

	public AIPlayer(string code, string name) : base(code, name) {
	}

    public abstract void notify(GameEngine engine);
}