using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

    public bool initialized;
    public GameEngine engine;

    public void initialize(GameEngine engine) {
        initialized = true;
        this.engine = engine;
    }

    public void Update() {
        if (!initialized) {
            return;
        }
        if (!gameObject.GetComponent<AudioSource>().isPlaying) {
            engine.postEvent(new GameEvent(gameObject.GetComponent<AudioSource>().clip.GetInstanceID().ToString(), "soundOver", "audioSource"));
        }
    }

}