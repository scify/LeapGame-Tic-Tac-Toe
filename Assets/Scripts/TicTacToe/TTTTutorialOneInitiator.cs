using UnityEngine;
using System.Collections.Generic;

public class TTTTutorialOneInitiator : MonoBehaviour {

    public float offset_x;
    public float offset_y;

    private int clip_id;

	void Start () {
        TTTStateRenderer renderer = new TTTStateRenderer();
        AudioEngine auEngine = new AudioEngine(0, "Tic-Tac-Toe", Settings.menu_sounds, Settings.game_sounds);

        List<Actor> actors = new List<Actor>();
        actors.Add(new TTTActor("cursor", "Prefabs/TTT/Cursor", new Vector3(0, 0, 0), false, (WorldObject wo, GameEngine engine) => {
            if (wo is TTTStaticObject && engine is TTTGameEngine) {
                if ((wo as TTTStaticObject).prefab.Contains("TTT/O")) {
                    engine.getState().timestamp++;
                    AudioClip audioClip = auEngine.getSoundForPlayer("ofilled", new Vector3(wo.position.x / offset_x, wo.position.z / offset_y, 0));
                    (engine as TTTGameEngine).state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, wo.position);
                    (engine as TTTGameEngine).state.environment.Add((engine as TTTGameEngine).state.blockingSound);
                    clip_id = audioClip.GetInstanceID();
                } else if ((wo as TTTStaticObject).prefab.Contains("TTT/X")) {
                    engine.getState().timestamp++;
                    AudioClip audioClip = auEngine.getSoundForPlayer("xfilled", new Vector3(wo.position.x / offset_x, wo.position.z / offset_y, 0));
                    (engine as TTTGameEngine).state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, wo.position);
                    (engine as TTTGameEngine).state.environment.Add((engine as TTTGameEngine).state.blockingSound);
                    clip_id = audioClip.GetInstanceID();
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

        List<Player> players = new List<Player>();
        players.Add(new Player("player0", "Nick"));

        TTTRuleset rules = new TTTRuleset();
        rules.Add(new TTTRule("initialization", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            AudioClip audioClip = auEngine.getSoundForMenu("intro1_text1");
            state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero);
            state.environment.Add(state.blockingSound);
            state.timestamp = 1;
            clip_id = audioClip.GetInstanceID();
            return false;
        }));

        rules.Add(new TTTRule("action", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (eve.payload.Equals("escape")) {
                Application.LoadLevel("mainMenu");
                return false;
            }
            return true;
        }));

        rules.Add(new TTTRule("soundSettings", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            Settings.game_sounds = eve.payload;
            auEngine = new AudioEngine(0, "Tic-Tac-Toe", Settings.menu_sounds, Settings.game_sounds);
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
            AudioClip audioClip;
            if (eve.payload.Equals(clip_id.ToString())) {
                switch (engine.state.timestamp) {
                    case 1:
                        engine.state.timestamp = 2;
                        break;
                    case 20:
                        engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro1_text2"), Vector3.zero);
                        engine.state.environment.Add(engine.state.blockingSound);
                        engine.state.timestamp = 21;
                        break;
                    case 24:
                        audioClip = auEngine.getSoundForMenu("intro1_text3");
                        engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero);
                        engine.state.environment.Add(engine.state.blockingSound);
                        engine.state.timestamp = 25;
                        clip_id = audioClip.GetInstanceID();
                        break;
                    case 25:
                        audioClip = auEngine.getSoundForPlayer("xfilled", Vector3.zero);
                        engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero);
                        engine.state.environment.Add(engine.state.blockingSound);
                        engine.state.timestamp = 3;
                        clip_id = audioClip.GetInstanceID();
                        break;
                    case 3:
                        audioClip = auEngine.getSoundForMenu("intro1_text4");
                        engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero);
                        engine.state.environment.Add(engine.state.blockingSound);
                        engine.state.timestamp = 35;
                        clip_id = audioClip.GetInstanceID();
                        break;
                    case 35:
                        audioClip = auEngine.getSoundForPlayer("xfilled", Vector3.zero);
                        engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero);
                        engine.state.environment.Add(engine.state.blockingSound);
                        engine.state.timestamp = 4;
                        clip_id = audioClip.GetInstanceID();
                        break;
                    case 4:
                        engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro1_text5"), Vector3.zero);
                        engine.state.environment.Add(engine.state.blockingSound);
                        engine.state.timestamp = 5;
                        break;
                    case 55:
                        engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro1_text6"), Vector3.zero);
                        engine.state.environment.Add(engine.state.blockingSound);
                        engine.state.timestamp = 6;
                        break;
                    case 7:
                        engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro1_text7"), Vector3.zero);
                        engine.state.environment.Add(engine.state.blockingSound);
                        engine.state.timestamp = 10;
                        (engine.state.result as TTTGameResult).status = TTTGameResult.GameStatus.Won;
                        break;
                }
            }
            return false;
        }));

        rules.Add(new TTTRule("ALL", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (state.blockingSound != null) {
                return false;
            }
            return true;
        }));

        rules.Add(new TTTRule("action", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if ((engine.state.result as TTTGameResult).status == TTTGameResult.GameStatus.Won && eve.payload.Equals("enter")) {
                (state.result as TTTGameResult).status = TTTGameResult.GameStatus.Over;
                return false;
            }
            return true;
        }));

        rules.Add(new TTTRule("action", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (engine.getState().timestamp == 5 && eve.payload.Equals("select")) {
                engine.state.timestamp = 55;
                Actor ac = state.actors[0];
                float x = ac.position.x / offset_x >= 0 ? ac.position.x - offset_x : ac.position.x + offset_x;
                float z = ac.position.z / offset_y >= 0 ? ac.position.z - offset_y : ac.position.z + offset_y;
                engine.state.environment.Add(new TTTStaticObject("Prefabs/TTT/X", new Vector3(x, 0, z), false));
                Actor actor = engine.state.actors[0];
                AudioClip audioClip = auEngine.getSoundForPlayer("empty", new Vector3(actor.position.x / offset_x, actor.position.z / offset_y, 0));
                engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, actor.position);
                engine.state.environment.Add(engine.state.blockingSound);
                clip_id = audioClip.GetInstanceID();
                return false;
            }
            if (eve.payload.Equals("select") && engine.state.timestamp >= 5) {
                foreach (Actor actor in state.actors) {
                    bool found = false;
                    foreach (WorldObject wo in state.environment) {
                        if (wo.position == actor.position) {
                            actor.interact(wo, engine);
                            found = true;
                            break;
                        }
                    }
                    if (!found) {
                        AudioClip audioClip = auEngine.getSoundForPlayer("empty", new Vector3(actor.position.x / offset_x, actor.position.z / offset_y, 0));
                        engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, actor.position);
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
                        AudioClip audioClip = auEngine.getSoundForPlayer("boundary", new Vector3(dx, -dy, 0));
                        engine.state.environment.Add(new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, actor.position));
                        if (state.timestamp > 20 && state.timestamp < 24) {
                            state.timestamp++;
                            if (state.timestamp == 24) {
                                clip_id = audioClip.GetInstanceID();
                            }
                        }
                        if (state.timestamp == 2) {
                            state.timestamp = 20;
                            clip_id = audioClip.GetInstanceID();
                        }
                        return false;
                    }
                    engine.state.environment.Add(new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForPlayer("just moved", new Vector3(x, y, 0)), actor.position));
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