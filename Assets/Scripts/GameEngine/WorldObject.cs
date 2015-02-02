/**
 * The file for the WorldObject class. 
 * 
 * This file holds the definition and implementation 
 * of WorldObject class. This file is part of
 * the LEAP project. 
 * 
 * @file WorldObject.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Nick Zorbas
 * @copyright ??? distributed as is under MIT Licence.
 */
using UnityEngine;
using System.Collections;

public abstract class WorldObject {

    public Vector3 position;
    public bool hidden;

	public WorldObject(Vector3 position, bool hidden) {
        this.position = position;
        this.hidden = hidden;
	}

    public void move(Vector3 offset) {
        position += offset;
    }

    public Vector3 getPosition() {
        return position;
    }
	
}