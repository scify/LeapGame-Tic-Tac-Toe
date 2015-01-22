using UnityEngine;
using System.Collections.Generic;

public class TTTTutorialMenuInitiator : MonoBehaviour {

    public float offset_x;
    public float offset_y;

    void Start() {
        TTTStateRenderer renderer = new TTTStateRenderer();
        AudioEngine auEngine = new AudioEngine(0, "Tic-Tac-Toe");

        List<WorldObject> environment = new List<WorldObject>();
        environment.Add(new TTTStaticObject("Prefabs/TTT/Camera_Default", new Vector3(0, 10, 0), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Light_Default", new Vector3(0, 10, 0), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Logo", new Vector3(-2 * offset_x, 0, -offset_y), false));
        environment.Add(new TTTMenuItem("Prefabs/TTT/ButtonSelected", "Χώρος", "tutorialOne", auEngine.getSound("xfilled", new Vector3(0, 0, 0)), new Vector3(0, 0, -offset_y), false, true));
        environment.Add(new TTTMenuItem("Prefabs/TTT/ButtonDefault", "Κανόνες", "tutorialTwo", auEngine.getSound("xfilled", new Vector3(0, 0, 0)), new Vector3(0, 0, 0), false));
        environment.Add(new TTTMenuItem("Prefabs/TTT/ButtonDefault", "Πίσω", "mainMenu", auEngine.getSound("xfilled", new Vector3(0, 0, 0)), new Vector3(0, 0, offset_y), false));

        TTTRuleset rules = new TTTRuleset();
        rules.Add(new TTTRule("initialization", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            //TODO: Play intro
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

        rules.Add(new TTTRule("action", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            if (eve.payload.Equals("select")) {
                foreach (WorldObject obj in state.environment) {
                    if (obj is TTTMenuItem) {
                        if ((obj as TTTMenuItem).selected) {
                            foreach (TTTSoundObject so in state.stoppableSounds) {
                                state.environment.Remove(so);
                            }
                            state.stoppableSounds.Clear();
                            //TODO: play sound
                            return false;
                        }
                    }
                }
            }
            return true;
        }));

        rules.Add(new TTTRule("move", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            TTTMenuItem previous = null;
            bool change = false;
            foreach (WorldObject obj in state.environment) {
                if (obj is TTTMenuItem) {
                    TTTMenuItem temp = obj as TTTMenuItem;
                    if (temp.selected) {
                        if (eve.payload == "_up" || eve.payload == "left") {
                            if (previous == null) {
                                foreach (TTTSoundObject so in state.stoppableSounds) {
                                    state.environment.Remove(so);
                                }
                                state.stoppableSounds.Clear();
                                AudioClip audioClip = auEngine.getSound("boundary", new Vector3(1, 0, 0));
                                engine.state.environment.Add(new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero));
                                break;
                            }
                            temp.selected = false;
                            temp.prefab = temp.prefab.Replace("Selected", "Default");
                            previous.selected = true;
                            previous.prefab = previous.prefab.Replace("Default", "Selected");
                            foreach (TTTSoundObject so in state.stoppableSounds) {
                                state.environment.Remove(so);
                            }
                            state.stoppableSounds.Clear();
                            //TODO: Play sound
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
                        foreach (TTTSoundObject so in state.stoppableSounds) {
                            state.environment.Remove(so);
                        }
                        state.stoppableSounds.Clear();
                        //TODO: Play sound
                        break;
                    }
                    previous = temp;
                }
            }
            if (change) {
                foreach (TTTSoundObject so in state.stoppableSounds) {
                    state.environment.Remove(so);
                }
                state.stoppableSounds.Clear();
                AudioClip audioClip = auEngine.getSound("boundary", new Vector3(0, 1, 0));
                engine.state.environment.Add(new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero));
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
