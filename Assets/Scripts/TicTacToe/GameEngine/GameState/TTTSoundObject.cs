using UnityEngine;
using System.Collections;

public class TTTSoundObject : SoundObject, IUnityRenderable {

    public string prefab;

    public TTTSoundObject(string prefab, Vector3 position) : base(position, false) {
        this.prefab = prefab;
        this.position = position;
	}

    public string getPrefab() {
        return prefab;
    }

}