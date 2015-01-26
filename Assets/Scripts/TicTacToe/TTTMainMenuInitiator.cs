using UnityEngine;
using System.Collections.Generic;

public class TTTMainMenuInitiator : MonoBehaviour {

    public float offset_x;
    public float offset_y;

    void Start() {
        if (!Settings.initialized) {
            Settings.initialized = true;
            Settings.game_sounds = "no repeat";
            Settings.menu_sounds = "default";
        }

        TTTStateRenderer renderer = new TTTStateRenderer();
        AudioEngine auEngine = new AudioEngine(0, "Tic-Tac-Toe", Settings.menu_sounds, Settings.game_sounds);

        List<WorldObject> environment = new List<WorldObject>();
        environment.Add(new TTTStaticObject("Prefabs/TTT/Camera_Default", new Vector3(0, 10, 0), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Light_Default", new Vector3(0, 10, 0), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Logo", new Vector3(-2 * offset_x, 0, -offset_y), false));
        environment.Add(new TTTMenuItem("Prefabs/TTT/ButtonSelected", "Νέο Παιχνίδι", "newGame", "new_game", auEngine.getSoundForMenu("new_game"), new Vector3(0, 0, -offset_y), false, true));
        environment.Add(new TTTMenuItem("Prefabs/TTT/ButtonDefault", "Οδηγίες", "tutorialMenu", "tutorials", auEngine.getSoundForMenu("tutorials"), new Vector3(0, 0, 0), false));
        environment.Add(new TTTMenuItem("Prefabs/TTT/ButtonDefault", "Έξοδος", "exitScene", "exit", auEngine.getSoundForMenu("exit"), new Vector3(0, 0, offset_y), false));

        TTTRuleset rules = new TTTRuleset();
        rules.Add(new TTTRule("initialization", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            AudioClip audioClip;
            if (Settings.just_started) {
                Settings.just_started = false;
                audioClip = auEngine.getSoundForMenu("game_intro");
                state.timestamp = 0;
            } else {
                audioClip = auEngine.getSoundForMenu("new_game");
                state.timestamp = 1;
            }
            TTTSoundObject tso = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero);
            state.environment.Add(tso);
            state.stoppableSounds.Add(tso);
            return false;
        }));

        rules.Add(new TTTRule("soundSettings", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            Settings.menu_sounds = eve.payload;
            auEngine = new AudioEngine(0, "Tic-Tac-Toe", Settings.menu_sounds, Settings.game_sounds);
            foreach (WorldObject wo in state.environment) {
                if (wo is TTTMenuItem) {
                    (wo as TTTMenuItem).audioMessage = auEngine.getSoundForMenu((wo as TTTMenuItem).audioMessageCode);
                }
            }
            return false;
        }));

        rules.Add(new TTTRule("soundOver", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            if (state.timestamp == 0) {
                AudioClip audioClip = auEngine.getSoundForMenu("new_game");
                TTTSoundObject tso = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero);
                state.environment.Add(tso);
                state.stoppableSounds.Add(tso);
                state.timestamp = 1;
            }
            return false;
        }));

        rules.Add(new TTTRule("action", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            if (eve.payload.Equals("escape")) {
                Application.LoadLevel("mainMenu");
                return false;
            }
            return true;
        }));

        rules.Add(new TTTRule("action", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            if (eve.payload.Equals("enter")) {
                foreach (WorldObject obj in state.environment) {
                    if (obj is TTTMenuItem) {
                        if ((obj as TTTMenuItem).selected) {
                            Application.LoadLevel((obj as TTTMenuItem).target);
                            return false;
                        }
                    }
                }
            }
            return true;
        }));

        rules.Add(new TTTRule("move", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            state.timestamp++;
            foreach (TTTSoundObject tttso in state.stoppableSounds) {
                state.environment.Remove(tttso);
            }
            state.stoppableSounds.Clear();
            TTTMenuItem previous = null;
            bool change = false;
            AudioClip audioClip;
            TTTSoundObject tso;
            foreach (WorldObject obj in state.environment) {
                if (obj is TTTMenuItem) {
                    TTTMenuItem temp = obj as TTTMenuItem;
                    if (temp.selected) {
                        if (eve.payload == "_up" || eve.payload == "left") {
                            if (previous == null) {
                                audioClip = auEngine.getSoundForMenu("boundary");
                                tso = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero);
                                state.environment.Add(tso);
                                state.stoppableSounds.Add(tso);
                                break;
                            }
                            temp.selected = false;
                            temp.prefab = temp.prefab.Replace("Selected", "Default");
                            previous.selected = true;
                            previous.prefab = previous.prefab.Replace("Default", "Selected");
                            tso = new TTTSoundObject("Prefabs/TTT/AudioSource", previous.audioMessage, Vector3.zero);
                            state.environment.Add(tso);
                            state.stoppableSounds.Add(tso);
                            break;
                        } else {
                            change = true;
                        }
                    } else if (change) {
                        temp.selected = true;
                        temp.prefab = temp.prefab.Replace("Default", "Selected");
                        previous.prefab = previous.prefab.Replace("Selected", "Default");
                        previous.selected = false;
                        change = false;
                        tso = new TTTSoundObject("Prefabs/TTT/AudioSource", temp.audioMessage, Vector3.zero);
                        state.environment.Add(tso);
                        state.stoppableSounds.Add(tso);
                        break;
                    }
                    previous = temp;
                }
            }
            if (change) {
                audioClip = auEngine.getSoundForMenu("boundary");
                tso = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero);
                state.environment.Add(tso);
                state.stoppableSounds.Add(tso);
            }
            return true;
        }));

        gameObject.AddComponent<TTTMenuEngine>();
        gameObject.AddComponent<TTTMenuUserInterface>();
        gameObject.GetComponent<TTTMenuEngine>().initialize(rules, environment, renderer);
        gameObject.GetComponent<TTTMenuUserInterface>().initialize(gameObject.GetComponent<TTTMenuEngine>());
        gameObject.GetComponent<TTTMenuEngine>().postEvent(new GameEvent("", "initialization", "unity"));
    }
}
