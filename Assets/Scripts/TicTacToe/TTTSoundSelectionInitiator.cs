
/**
 * Copyright 2016 , SciFY NPO - http://www.scify.org
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using UnityEngine;
using System.Collections.Generic;

public class TTTSoundSelectionInitiator : MonoBehaviour {

    public float offset_y;
    private TTTStaticObject movingCamera = new TTTStaticObject("Prefabs/TTT/Camera_Default", new Vector3(0, 10, 0), false);

    void Start() {
        TTTStateRenderer renderer = new TTTStateRenderer();
        AudioEngine auEngine = new AudioEngine(0, "Tic-Tac-Toe", Settings.menu_sounds, Settings.game_sounds);

        List<WorldObject> environment = new List<WorldObject>();
        environment.Add(movingCamera);
        environment.Add(new TTTStaticObject("Prefabs/TTT/Light_Default", new Vector3(0, 10, 0), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Logos", new Vector3(10000, 0, 0), false));
        int i = 0;

        foreach (string s in auEngine.getSettingsAudioForMenu()) {
            if (s == Settings.default_soundset) {
                environment.Add(new TTTMenuItem("Prefabs/TTT/ButtonSelected", Settings.default_soundset_btn_name,  s, s + "_", null, new Vector3(0, 0, -offset_y), false, true));
            } else {
                //environment.Add(new TTTMenuItem("Prefabs/TTT/ButtonDefault", s, s, s + "_", null, new Vector3(0, 0, i++ * offset_y), false, false));
            }
        }
        // load main menu screen
        Application.LoadLevel("mainMenu");

        TTTRuleset rules = new TTTRuleset();
        rules.Add(new TTTRule("initialization", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            AudioClip audioClip;
            audioClip = auEngine.getSoundForMenu("voice_selection");
            state.timestamp = 0;
            TTTSoundObject tso = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero);
            state.environment.Add(tso);
            state.stoppableSounds.Add(tso);
            Settings.previousMenu = "mainMenu";
            return false;
        }));

        rules.Add(new TTTRule("action", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            if (eve.payload.Equals("enter")) {
                foreach (WorldObject obj in state.environment) {
                    if (obj is TTTMenuItem) {
                        TTTMenuItem temp = obj as TTTMenuItem;
                        if (temp.selected) {
                            Settings.menu_sounds = temp.target;
                            Settings.game_sounds = temp.target;
                        }
                    }
                }
                Application.LoadLevel("mainMenu");
            }
            return true;
        }));

        rules.Add(new TTTRule("move", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            state.timestamp++;
            foreach (TTTSoundObject TTTSo in state.stoppableSounds) {
                state.environment.Remove(TTTSo);
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
                                audioClip = auEngine.getSoundForPlayer("boundary", Vector3.up);
                                tso = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero);
                                state.environment.Add(tso);
                                state.stoppableSounds.Add(tso);
                                break;
                            }
                            temp.selected = false;
                            temp.prefab = temp.prefab.Replace("Selected", "Default");
                            previous.selected = true;
                            previous.prefab = previous.prefab.Replace("Default", "Selected");
                            tso = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForPlayer(previous.audioMessageCode + "1"), Vector3.zero);
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
                        tso = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForPlayer(temp.audioMessageCode + "1"), Vector3.zero);
                        state.environment.Add(tso);
                        state.stoppableSounds.Add(tso);
                        break;
                    }
                    previous = temp;
                }
            }
            if (change) {
                audioClip = auEngine.getSoundForPlayer("boundary", Vector3.down);
                tso = new TTTSoundObject("Prefabs/TTT/AudioSource", audioClip, Vector3.zero);
                state.environment.Add(tso);
                state.stoppableSounds.Add(tso);
            }
            foreach (WorldObject obj in state.environment) {
                if (obj is TTTMenuItem) {
                    TTTMenuItem temp = obj as TTTMenuItem;
                    if (temp.selected) {
                        movingCamera.position = new Vector3(0, 10, Mathf.Clamp(temp.position.z, 6 * offset_y, 0));
                        break;
                    }
                }
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