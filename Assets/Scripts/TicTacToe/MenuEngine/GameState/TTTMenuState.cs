
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

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TTTMenuState : GameState {

    public List<TTTSoundObject> stoppableSounds = new List<TTTSoundObject>();

    public TTTMenuState(List<WorldObject> environment) {
        timestamp = 0;
        this.actors = new List<Actor>();
        this.environment = environment;
        this.players = new List<Player>();
        curPlayer = 0;
        result = new TTTMenuResult(TTTMenuResult.GameStatus.Choosing);
    }

    public TTTMenuState(TTTMenuState previousState) {
        timestamp = previousState.timestamp;
        actors = new List<Actor>();
        players = new List<Player>();
        environment = new List<WorldObject>(previousState.environment);
        curPlayer = previousState.curPlayer;
    }
}