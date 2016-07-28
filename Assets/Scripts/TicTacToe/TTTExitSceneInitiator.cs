
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

public class TTTExitSceneInitiator : MonoBehaviour {

    public float offset_x;
    public float offset_y;

    void Start() {
        TTTStateRenderer renderer = new TTTStateRenderer();
        AudioEngine auEngine = new AudioEngine(0, "Tic-Tac-Toe", Settings.menu_sounds, Settings.game_sounds);

        List<WorldObject> environment = new List<WorldObject>();
        environment.Add(new TTTStaticObject("Prefabs/TTT/Camera_Default", new Vector3(0, 10, 0), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Light_Default", new Vector3(0, 10, 0), false));
        environment.Add(new CanvasObject("Prefabs/TTT/OutroLogo", true, new Vector3(0, 0, 0), false));

        TTTRuleset rules = new TTTRuleset();
        rules.Add(new TTTRule("initialization", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            TTTSoundObject tso = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("outro"), Vector3.zero);
            state.environment.Add(tso);
            state.stoppableSounds.Add(tso);
            return false;
        }));

        rules.Add(new TTTRule("soundOver", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            Application.Quit();
            return false;
        }));

        gameObject.AddComponent<TTTMenuEngine>();
        gameObject.AddComponent<TTTMenuUserInterface>();
        gameObject.GetComponent<TTTMenuEngine>().initialize(rules, environment, renderer);
        gameObject.GetComponent<TTTMenuUserInterface>().initialize(gameObject.GetComponent<TTTMenuEngine>());
        gameObject.GetComponent<TTTMenuEngine>().postEvent(new GameEvent("", "initialization", "unity"));
    }
}
