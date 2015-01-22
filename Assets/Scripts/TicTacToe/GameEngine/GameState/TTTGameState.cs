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