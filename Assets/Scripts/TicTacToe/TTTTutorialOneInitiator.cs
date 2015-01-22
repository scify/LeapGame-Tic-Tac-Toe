using UnityEngine;
using System.Collections.Generic;

public class TTTTutorialOneInitiator : MonoBehaviour {

    public float offset_x;
    public float offset_y;

	void Start () {

        TTTStateRenderer renderer = new TTTStateRenderer();
        AudioEngine auEngine = new AudioEngine(0, "Tic-Tac-Toe");

        List<Actor> actors = new List<Actor>();
        actors.Add(new TTTActor("cursor", "Prefabs/TTT/Cursor", new Vector3(0, 0, 0), false, (WorldObject wo, GameEngine engine) => {
            if (wo is TTTStaticObject && engine is TTTGameEngine) {
                if ((wo as TTTStaticObject).prefab.Contains("TTT/O")) {
                    AudioClip audioClip = auEngine.getSound("ofilled", new Vector3(wo.position.x / offset_x, wo.position.z / offset_y, 0));
                    (engine as TTTGameEngine).state.environment.Add(new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, wo.position));
                } else if ((wo as TTTStaticObject).prefab.Contains("TTT/X")) {
                    AudioClip audioClip = auEngine.getSound("xfilled", new Vector3(wo.position.x / offset_x, wo.position.z / offset_y, 0));
                    (engine as TTTGameEngine).state.environment.Add(new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, wo.position));
                }
            }
        }));

        List<WorldObject> environment = new List<WorldObject>();
        environment.Add(new TTTStaticObject("Prefabs/TTT/Camera_Default", new Vector3(0, 10, 0), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Light_Default", new Vector3(0, 10, 0), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Line_Horizontal", new Vector3(0, 0, offset_y / 2), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Line_Horizontal", new Vector3(0, 0, -offset_y / 2), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Line_Vertical", new Vector3(offset_x / 2, 0, 0), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Line_Vertical", new Vector3(-offset_x / 2, 0, 0), false));

        List<KeyValuePair<int, int>> points = new List<KeyValuePair<int, int>>();
        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                points.Add(new KeyValuePair<int, int>(i, j));
            }
        }
        int k;
        for (int i = 0; i < 5; i++) {
            k = Random.Range(0, 9 - i);
            environment.Add(new TTTStaticObject("Prefabs/TTT/O", new Vector3(offset_x * points[k].Key, 0, offset_y * points[k].Value), false));
            points.RemoveAt(k);
        }
        k = Random.Range(0, 4);
        environment.Add(new TTTStaticObject("Prefabs/TTT/X", new Vector3(offset_x * points[k].Key, 0, offset_y * points[k].Value), false));
        points.RemoveAt(k);

        List<Player> players = new List<Player>();
        players.Add(new Player("player0", "Nick"));

        TTTRuleset rules = new TTTRuleset();
        rules.Add(new TTTRule("action", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (eve.payload.Equals("escape")) {
                Application.LoadLevel("mainMenu");
                return false;
            }
            return true;
        }));

        rules.Add(new TTTRule("soundOver", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            int id = int.Parse(eve.payload);
            if (state.blockingSound != null && id == state.blockingSound.clip.GetInstanceID()) {
                state.environment.Remove(state.blockingSound);
                state.blockingSound = null;
            } else {
                WorldObject toRemove = null;
                foreach (WorldObject go in state.environment) {
                    if (go is TTTSoundObject && (go as TTTSoundObject).clip.GetInstanceID() == id) {
                        toRemove = go;
                        break;
                    }
                }
                if (toRemove != null) {
                    state.environment.Remove(toRemove);
                }
            }
            switch (engine.state.timestamp) {
                case 0:
                    engine.state.timestamp = 1;
                    break;
                case 1:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSound("selected", Vector3.zero), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    engine.state.timestamp = 2;
                    break;
                case 2:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSound("selected", Vector3.zero), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    engine.state.timestamp = 25;
                    break;
                case 25:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSound("ofilled", Vector3.down), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    engine.state.timestamp = 3;
                    break;
                case 3:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSound("selected", Vector3.zero), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    engine.state.timestamp = 35;
                    break;
                case 35:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSound("xfilled", Vector3.down), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    engine.state.timestamp = 4;
                    break;
                case 4:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSound("selected", Vector3.zero), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    engine.state.timestamp = 5;
                    break;
                case 5:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSound("selected", Vector3.zero), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    engine.state.timestamp = 6;
                    break;
            }
            return false;
        }));

        rules.Add(new TTTRule("ALL", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (state.blockingSound != null) {
                return false;
            }
            return true;
        }));

        rules.Add(new TTTRule("initialization", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSound("selected", Vector3.zero), Vector3.zero);
            engine.state.environment.Add(engine.state.blockingSound);
            engine.state.timestamp = 0;
            return false;
        }));

        rules.Add(new TTTRule("action", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (state.timestamp == 10 && eve.payload.Equals("enter")) {
                Application.LoadLevel("mainMenu");
            }
            if (state.timestamp == 10 && eve.payload.Equals("select")) {
                Application.LoadLevel(Application.loadedLevel);
                return false;
            }
            return true;
        }));

        rules.Add(new TTTRule("action", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (eve.payload.Equals("select") && engine.state.timestamp >= 6) {
                foreach (Actor actor in state.actors) {
                    foreach (WorldObject wo in state.environment) {
                        if (wo.position == actor.position) {
                            actor.interact(wo, engine);
                            break;
                        }
                    }
                }
            }
            return true;
        }));

        rules.Add(new TTTRule("action", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (eve.payload.Equals("enter")) {
                foreach (Actor actor in state.actors) {
                    bool overlap = false;
                    foreach (WorldObject wo in state.environment) {
                        if (wo.position == actor.position && !(wo is TTTSoundObject)) {
                            overlap = (wo as IUnityRenderable).getPrefab().Contains("TTT/X");
                        }
                    }
                    AudioClip audioClip;
                    if (overlap) {
                        audioClip = auEngine.getSound("selected", new Vector3(actor.position.x / offset_x, actor.position.z / offset_y, 0));
                        engine.state.environment.Add(new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, actor.position));
                        engine.state.timestamp = 10;
                    } else {
                        engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSound("selected", Vector3.zero), Vector3.zero);
                        engine.state.environment.Add(engine.state.blockingSound);
                    }
                }
            }
            return true;
        }));

        rules.Add(new TTTRule("move", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (eve.payload.Length == 2) {
                int xy = int.Parse(eve.payload);
                int x = (xy % 10) - 1;
                int y = (xy / 10) - 1;
                foreach (Actor actor in state.actors) {
                    actor.position = new Vector3(offset_x * x, 0, offset_y * y);
                }
            }
            return true;
        }));

        rules.Add(new TTTRule("move", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (eve.payload.Length != 2) {
                int dx = 0;
                int dy = 0;
                switch (eve.payload) {
                    case "_up":
                        dy = -1;
                        break;
                    case "down":
                        dy = 1;
                        break;
                    case "left":
                        dx = -1;
                        break;
                    case "right":
                        dx = 1;
                        break;
                    default:
                        break;
                }
                foreach (Actor actor in state.actors) {
                    int x = (int)(actor.position.x / offset_x) + dx;
                    int y = (int)(actor.position.z / offset_y) + dy;
                    if (x < -1 || x > 1 || y < -1 || y > 1) {
                        AudioClip audioClip = auEngine.getSound("boundary", new Vector3(dx, dy, 0));
                        engine.state.environment.Add(new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, actor.position));
                        return false;
                    }
                    actor.position = new Vector3(offset_x * x, 0, offset_y * y);
                }
            }
            return true;
        }));

        gameObject.AddComponent<TTTGameEngine>();
        gameObject.AddComponent<TTTUserInterface>();
        gameObject.GetComponent<TTTGameEngine>().initialize(rules, actors, environment, players, renderer);
        gameObject.GetComponent<TTTUserInterface>().initialize(gameObject.GetComponent<TTTGameEngine>());
        gameObject.GetComponent<TTTGameEngine>().postEvent(new GameEvent("", "initialization", "unity"));
	}
}