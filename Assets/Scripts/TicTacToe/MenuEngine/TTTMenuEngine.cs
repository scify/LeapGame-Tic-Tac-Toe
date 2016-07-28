
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
using System.Collections.Generic;
using System;

public class TTTMenuEngine : GameEngine {

    public new TTTStateRenderer renderer;
    public TTTRuleset rules;
    public TTTMenuState state;
    public List<WorldObject> environment;
    public Queue<GameEvent> events;

    private bool initialized = false;

    public void initialize(TTTRuleset rules, List<WorldObject> environment, TTTStateRenderer renderer) {
        this.rules = rules;
        this.environment = environment;
        this.renderer = renderer;
        state = new TTTMenuState(environment);
        events = new Queue<GameEvent>();
        state.curPlayer = -1;
        initialized = true;
    }

	public void Start() {
        run();
	}

    public override void run() {
    }

    public void Update() {
        loop();
    }

    public override void loop() {
        if (!initialized) {
            return;
        }
        while (events.Count != 0) {
            if (state.result.gameOver()) {
                cleanUp();
                return;
            }
            GameEvent curEvent = events.Dequeue();
            rules.applyTo(state, curEvent, this);
        }
        renderer.render(this);
        if (state.result.gameOver()) {
            cleanUp();
        }
	}

    public override void postEvent(GameEvent eve) {
        events.Enqueue(eve);
    }

    public override GameState getState() {
        return state;
    }

    public override StateRenderer getRenderer() {
        return renderer;
    }

    public override void cleanUp() {
        Application.LoadLevel(Settings.previousMenu);
	}

}