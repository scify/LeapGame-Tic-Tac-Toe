
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
using System;

public class TTTMenuRuleset : Ruleset<TTTMenuRule> {

    public override void applyTo(GameState state, GameEvent eve, GameEngine engine) {
        throw new ArgumentException("Invalid game state type! Expected a TTTGameState, got " + state.GetType().ToString(), "state");
    }

    public void applyTo(TTTMenuState state, GameEvent eve, TTTMenuEngine engine) {
        List<TTTMenuRule> rules = this.FindAll(delegate(TTTMenuRule rule) {
            return rule.category.Equals(eve.type) || rule.category.Equals("ALL");
        });
        foreach (TTTMenuRule rule in rules) {
            if (!rule.applyTo(state, eve, engine)) {
                return;
            }
        };
    }

}