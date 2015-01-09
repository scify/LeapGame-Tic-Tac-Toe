using UnityEngine;
using System.Collections;

public class TTTActor : Actor, IUnityRenderable {

    public string code;
    public string prefab;
    public Interaction interaction;

    public delegate void Interaction(WorldObject target, GameEngine engine);

    public TTTActor(string code, string prefab, Vector3 position, bool hidden, Interaction interaction) : base(position, hidden) {
        this.prefab = prefab;
        this.code = code;
        this.interaction = interaction;
	}

    public string getPrefab() {
        return prefab;
    }

    public override void interact(WorldObject target, GameEngine engine) {
        interaction(target, engine);
    }

}