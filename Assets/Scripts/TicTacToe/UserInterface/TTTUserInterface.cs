using UnityEngine;
using System.Collections;

public class TTTUserInterface : MonoBehaviour {

    public TTTGameEngine engine;

    private bool initialized;

    public void initialize(TTTGameEngine engine) {
        this.engine = engine;
        initialized = true;
    }

	public void Update() {
        if (!initialized) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            engine.postEvent(new UIEvent("_up", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            engine.postEvent(new UIEvent("down", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            engine.postEvent(new UIEvent("left", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            engine.postEvent(new UIEvent("right", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            engine.postEvent(new UIEvent("20", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            engine.postEvent(new UIEvent("21", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            engine.postEvent(new UIEvent("22", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad4)) {
            engine.postEvent(new UIEvent("10", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad5)) {
            engine.postEvent(new UIEvent("11", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad6)) {
            engine.postEvent(new UIEvent("12", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad7)) {
            engine.postEvent(new UIEvent("00", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad8)) {
            engine.postEvent(new UIEvent("01", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad9)) {
            engine.postEvent(new UIEvent("02", "move", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            engine.postEvent(new UIEvent("escape", "action", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            engine.postEvent(new UIEvent("select", "action", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            engine.postEvent(new UIEvent("enter", "action", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            engine.postEvent(new UIEvent("default", "soundSettings", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            engine.postEvent(new UIEvent("pitch shifted", "soundSettings", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            engine.postEvent(new UIEvent("no repeat", "soundSettings", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            engine.postEvent(new UIEvent("funky", "soundSettings", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            engine.postEvent(new UIEvent("funky elevated", "soundSettings", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            engine.postEvent(new UIEvent("true", "autoSelect", "player0"));
        }

	}
	
}