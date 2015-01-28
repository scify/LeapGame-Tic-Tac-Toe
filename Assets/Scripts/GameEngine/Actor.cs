/**
 * The file for the Actor class. 
 * 
 * This file holds the definition and implementation 
 * of Actor class. This file is part of
 * the LEAP project. 
 * 
 * @file Actor.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Nick Zorbas
 * @copyright ??? distributed as is under MIT Licence.
 */
using UnityEngine;
using System.Collections;

public abstract class Actor : WorldObject {

    public Actor(Vector3 position, bool hidden) : base(position, hidden) {
	}
	
	public abstract void interact(WorldObject target, GameEngine engine);

}