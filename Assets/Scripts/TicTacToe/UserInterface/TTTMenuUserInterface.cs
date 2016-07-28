
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
using System.Collections;

public class TTTMenuUserInterface : MonoBehaviour {

    public TTTMenuEngine engine;

    private bool initialized;

    public void initialize(TTTMenuEngine engine) {
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
        if (Input.GetKeyDown(KeyCode.Space)) {
            engine.postEvent(new UIEvent("select", "action", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            engine.postEvent(new UIEvent("enter", "action", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            engine.postEvent(new UIEvent("escape", "action", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            engine.postEvent(new UIEvent("default", "soundSettings", "player0"));
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            engine.postEvent(new UIEvent("electra", "soundSettings", "player0"));
        }

	}
	
}