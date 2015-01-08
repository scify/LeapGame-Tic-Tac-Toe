using UnityEngine;
using System.Collections.Generic;
using System;

public class TTTAIPlayer : AIPlayer {

    public TTTAIPlayer(string code, string name) : base(code, name) {
	}

    public override void notify(GameEngine engine) {
        if (!(engine is TTTGameEngine)) {
            throw new ArgumentException("Invalid game engine type! Expected a TTTGameEngine, got " + engine.GetType().ToString());
        }
        if ((engine.getState().result as TTTGameResult).status == TTTGameResult.GameStatus.Ongoing && ("player" + engine.getState().curPlayer).Equals(code)) {
            List<KeyValuePair<int, int>> points = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if ((engine.getState() as TTTGameState).board[i, j] == -1) {
                        points.Add(new KeyValuePair<int, int>(i, j));
                    }
                }
            }
            KeyValuePair<int, int> point = points[new System.Random().Next(points.Count)];
            int x = point.Key;
            int y = point.Value;
            engine.postEvent(new GameEvent(x + "" + y, "move", code));
            engine.postEvent(new GameEvent("enter", "action", code));
        }
    }
}