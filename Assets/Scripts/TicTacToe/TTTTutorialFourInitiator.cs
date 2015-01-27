using UnityEngine;
using System.Collections.Generic;

public class TTTTutorialFourInitiator : MonoBehaviour {

    public float offset_x;
    public float offset_y;

    private Vector3 lastCursor = Vector3.zero;

    void Start() {
        TTTStateRenderer renderer = new TTTStateRenderer();
        AudioEngine auEngine = new AudioEngine(0, "Tic-Tac-Toe", Settings.menu_sounds, Settings.game_sounds);

        List<Actor> actors = new List<Actor>();
        actors.Add(new TTTActor("cursor", "Prefabs/TTT/Cursor", new Vector3(0, 0, 0), false, (WorldObject wo, GameEngine engine) => {
            if (wo is TTTStaticObject && engine is TTTGameEngine) {
                if ((wo as TTTStaticObject).prefab.Contains("TTT/O")) {
                    AudioClip audioClip = auEngine.getSoundForPlayer("ofilled", new Vector3(wo.position.x / offset_x, -wo.position.z / offset_y, 0));
                    (engine as TTTGameEngine).state.environment.Add(new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, wo.position));
                } else if ((wo as TTTStaticObject).prefab.Contains("TTT/X")) {
                    AudioClip audioClip = auEngine.getSoundForPlayer("xfilled", new Vector3(wo.position.x / offset_x, -wo.position.z / offset_y, 0));
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

        List<Player> players = new List<Player>();
        players.Add(new Player("player0", "player0"));
        players.Add(new TTTAIPlayer("player1", "AI", false));

        TTTRuleset rules = new TTTRuleset();
        rules.Add(new TTTRule("initialization", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            TTTSoundObject tso = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro4_text1"), Vector3.zero);
            state.environment.Add(tso);
            state.blockingSound = tso;
            state.timestamp = -5;
            state.curPlayer = 0;
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
            switch (engine.state.timestamp) {
                case -5:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForPlayer("ofilled", Vector3.zero), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    engine.state.timestamp = -4;
                    break;
                case -4:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro4_text2"), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    engine.state.timestamp = -3;
                    break;
                case -3:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForPlayer("ofilled", Vector3.zero), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    engine.state.timestamp = -2;
                    break;
                case -2:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro4_text3"), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    engine.state.timestamp = 11;
                    break;
                case 12:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro4_text4"), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    state.timestamp = 13;
                    break;
                case 14:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro4_text5"), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    state.timestamp = 2;
                    break;
                case 15:
                    state.timestamp = 16;
                    break;
                case 16:
                    engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro4_text9"), Vector3.zero);
                    engine.state.environment.Add(engine.state.blockingSound);
                    state.timestamp = 10;
                    state.curPlayer = 0;
                    break;
            }
            return false;
        }));

        rules.Add(new TTTRule("ALL", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            return !eve.initiator.StartsWith("player") || (eve.initiator.Equals("player" + state.curPlayer) && state.blockingSound == null);
        }));

        rules.Add(new TTTRule("action", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (state.timestamp == 10 && eve.payload.Equals("enter")) {
                (state.result as TTTGameResult).status = TTTGameResult.GameStatus.Over;
                return false;
            }
            return true;
        }));

        rules.Add(new TTTRule("action", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (eve.payload.Equals("select")) {
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
                            overlap = true;
                        }
                    }
                    AudioClip audioClip;
                    if (overlap) {
                        audioClip = auEngine.getSoundForPlayer("error", Vector3.zero);
                        engine.state.environment.Add(new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, actor.position));
                        break;
                    } else {
                        int x = (int)(actor.position.x / offset_x + 1);
                        int y = (int)(actor.position.z / offset_y + 1);
                        state.board[x, y] = state.curPlayer;
                        string symbol = state.curPlayer == 0 ? "X" : "O";
                        engine.state.environment.Add(new TTTStaticObject("Prefabs/TTT/" + symbol, actor.position, false));
                        audioClip = auEngine.getSoundForPlayer(symbol.ToLower() + "filled", new Vector3(actor.position.x / offset_x, -actor.position.z / offset_y, 0));
                        engine.state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, actor.position);
                        engine.state.environment.Add(engine.state.blockingSound);
                        state.curPlayer = engine.players.Count - state.curPlayer - 1;
                        Actor ac = state.actors[0];
                        ac.position += lastCursor;
                        lastCursor = ac.position - lastCursor;
                        ac.position -= lastCursor;
                        state.timestamp++;
                        break;
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
                        return false;
                    }
                    actor.position = new Vector3(offset_x * x, 0, offset_y * y);
                }
            }
            return true;
        }));

        rules.Add(new TTTRule("action", (TTTGameState state, GameEvent eve, TTTGameEngine engine) => {
            if (eve.payload.Equals("enter")) {
                bool finished = false;
                for (int i = 0; i < 3; i++) {
                    if (state.board[i, 0] == state.board[i, 1] && state.board[i, 0] == state.board[i, 2] && state.board[i, 0] != -1) {
                        finished = true;
                    }
                }
                for (int i = 0; i < 3; i++) {
                    if (state.board[0, i] == state.board[1, i] && state.board[0, i] == state.board[2, i] && state.board[0, i] != -1) {
                        finished = true;
                    }
                }
                if (state.board[0, 0] == state.board[1, 1] && state.board[0, 0] == state.board[2, 2] && state.board[0, 0] != -1) {
                    finished = true;
                }
                if (state.board[0, 2] == state.board[1, 1] && state.board[0, 2] == state.board[2, 0] && state.board[0, 2] != -1) {
                    finished = true;
                }
                if (finished) {
                    (state.result as TTTGameResult).winner = state.curPlayer;
                    (state.result as TTTGameResult).status = TTTGameResult.GameStatus.Won;
                    state.timestamp = 15;
                    if (state.curPlayer != 0) {
                        state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro4_text6"), Vector3.zero);
                    } else {
                        state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro4_text7"), Vector3.zero);
                    }
                    state.environment.Add(state.blockingSound);
                    state.curPlayer = 0;
                    return false;
                }
                if (state.timestamp == 9) {
                    (state.result as TTTGameResult).status = TTTGameResult.GameStatus.Draw;
                    state.timestamp = 15;
                    state.curPlayer = 0;
                    state.blockingSound = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("intro4_text8"), Vector3.zero);
                    state.environment.Add(state.blockingSound);
                    return false;
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