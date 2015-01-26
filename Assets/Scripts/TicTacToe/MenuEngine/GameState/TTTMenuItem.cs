using UnityEngine;
using System.Collections;

public class TTTMenuItem : StaticObject, IUnityRenderable {

    public string prefab;
    public bool selected;
    public string message;
    public string target;
    public string audioMessageCode;
    public AudioClip audioMessage;

    public TTTMenuItem(string prefab, string message, string target, string audioMessageCode, AudioClip audioMessage, Vector3 position, bool hidden, bool selected = false) : base(position, hidden) {
        this.prefab = prefab;
        this.message = message;
        this.target = target;
        this.audioMessageCode = audioMessageCode;
        this.audioMessage = audioMessage;
        this.selected = selected;
	}

    public string getPrefab() {
        return prefab;
    }

}