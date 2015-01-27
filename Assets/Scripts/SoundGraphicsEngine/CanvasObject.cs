using System;
using UnityEngine;

class CanvasObject : StaticObject, IUnityRenderable {

    public bool coversCamera;
    public string prefab;

    public CanvasObject(string prefab, bool coversCamera, Vector3 position, bool hidden) : base(position, hidden) {
        this.prefab = prefab;
        this.coversCamera = coversCamera;
	}

    public string getPrefab() {
        return prefab;
    }

}