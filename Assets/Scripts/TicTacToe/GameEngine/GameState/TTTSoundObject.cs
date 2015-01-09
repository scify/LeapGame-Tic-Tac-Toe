using UnityEngine;
using System.Collections;

public class TTTSoundObject : SoundObject, IUnityRenderable {

    public AudioClip clip;
    public string prefab;

    public TTTSoundObject(string prefab, AudioClip clip, Vector3 position) : base(position, false) {
        this.clip = clip;
        this.prefab = prefab;
        this.position = position;
	}

    public string getPrefab() {
        return prefab;
    }

}