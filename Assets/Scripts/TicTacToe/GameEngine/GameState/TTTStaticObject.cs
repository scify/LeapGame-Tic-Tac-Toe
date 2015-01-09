using UnityEngine;
using System.Collections;

public class TTTStaticObject : StaticObject, IUnityRenderable {

    public string prefab;

    public TTTStaticObject(string prefab, Vector3 position, bool hidden) : base(position, hidden) {
        this.prefab = prefab;
	}

    public string getPrefab() {
        return prefab;
    }

}