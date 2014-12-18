using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TTTGameState : GameState {

    public int[,] board = new int[3, 3];

	public TTTGameState() {
        timestamp = 0;
        actors = new List<Actor>();
        environment = new List<WorldObject>();
        players = new List<Player>();
        curPlayer = 0;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++ ) {
                board[i, j] = -1;
            }
        }
	}
}