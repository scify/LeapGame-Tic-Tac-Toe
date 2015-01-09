using UnityEngine;
using System.Collections;

public abstract class Actor : WorldObject {

    public Actor(Vector3 position, bool hidden) : base(position, hidden) {
	}
	
	public abstract void interact(WorldObject target, GameEngine engine);

}