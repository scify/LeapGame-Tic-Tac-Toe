using UnityEngine;
using System.Collections;

public abstract class Actor : WorldObject {

	public Actor() {
	}
	
	public abstract void interact(WorldObject target);

}