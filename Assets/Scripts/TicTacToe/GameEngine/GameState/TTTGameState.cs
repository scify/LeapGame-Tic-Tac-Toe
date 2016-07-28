
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
public class TTTGameState : GameState {

    public int[,] board = new int[3, 3];
    public TTTSoundObject blockingSound;

    public TTTGameState(List<Actor> actors, List<WorldObject> environment, List<Player> players) {
        timestamp = 0;
        this.actors = actors;
        this.environment = environment;
        this.players = players;
        curPlayer = 0;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                board[i, j] = -1;
            }
        }
        result = new TTTGameResult(TTTGameResult.GameStatus.Ongoing, -1);
        blockingSound = null;
    }

    public TTTGameState(TTTGameState previousState) {
        timestamp = previousState.timestamp;
        actors = new List<Actor>(previousState.actors);
        environment = new List<WorldObject>(previousState.environment);
        players = new List<Player>(previousState.players);
        curPlayer = previousState.curPlayer;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                board[i, j] = previousState.board[i, j];
            }
        }
        blockingSound = previousState.blockingSound;
    }
}