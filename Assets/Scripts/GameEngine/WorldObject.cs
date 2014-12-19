using UnityEngine;
using System.Collections;

public abstract class WorldObject {

    public Vector3 position;
    public bool hidden;

	public WorldObject() {
	}

    public void move(Vector3 offset) {
        position += offset;
    }

    public Vector3 getPosition() {
        return position;
    }
	
}