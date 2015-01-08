using UnityEngine;
using System.Collections;

public class TTTSoundObject : SoundObject, IUnityRenderable {

    public string clip;
    public string prefab;

    public TTTSoundObject(string prefab, string clip, Vector3 position) : base(position, false) {
        this.clip = clip;
        this.prefab = prefab;
        this.position = position;
	}

    public string getPrefab() {
        return prefab;
    }

}