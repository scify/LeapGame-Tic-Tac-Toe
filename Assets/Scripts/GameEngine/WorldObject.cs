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