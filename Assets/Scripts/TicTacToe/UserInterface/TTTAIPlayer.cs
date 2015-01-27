using UnityEngine;
using System.Collections.Generic;
using System;

public class TTTAIPlayer : AIPlayer {

    public bool smart;

    public TTTAIPlayer(string code, string name, bool smart) : base(code, name) {
        this.smart = smart;
	}

    public override void notify(GameEngine engine) {
        if (!(engine is TTTGameEngine)) {
            throw new ArgumentException("Invalid game engine type! Expected a TTTGameEngine, got " + engine.GetType().ToString());
        }
        if ((engine.getState().result as TTTGameResult).status == TTTGameResult.GameStatus.Ongoing && ("player" + engine.getState().curPlayer).Equals(code)) {
            if ((engine as TTTGameEngine).state.blockingSound != null) {
                return;
            }
            if (smart) {
                TTTGameState state = (engine as TTTGameEngine).state;
                for (int i = 0; i < 3; i++) {
                    bool[] ver = new bool[] {
                        state.board[i, 0] == state.board[i, 1] && state.board[i, 1] == 1 && state.board[i, 2] == -1,
                        state.board[i, 0] == state.board[i, 2] && state.board[i, 2] == 1 && state.board[i, 1] == -1,
                        state.board[i, 1] == state.board[i, 2] && state.board[i, 2] == 1 && state.board[i, 0] == -1,
                        state.board[0, i] == state.board[1, i] && state.board[1, i] == 1 && state.board[2, i] == -1,
                        state.board[0, i] == state.board[2, i] && state.board[2, i] == 1 && state.board[1, i] == -1,
                        state.board[1, i] == state.board[2, i] && state.board[2, i] == 1 && state.board[0, i] == -1
                    };
                    for (int j = 0; j < 3; j++) {
                        if (ver[j]) {
                            engine.postEvent(new GameEvent((2 - j) + "" + i, "move", code));
                            engine.postEvent(new GameEvent("enter", "action", code));
                            return;
                        }
                    }
                    for (int j = 3; j < 6; j++) {
                        if (ver[j]) {
                            engine.postEvent(new GameEvent(i + "" + (5 - j), "move", code));
                            engine.postEvent(new GameEvent("enter", "action", code));
                            return;
                        }
                    }
                }
                bool[] diag = new bool[] {
                    state.board[0, 0] == state.board[1, 1] && state.board[0, 0] == 1 && state.board[2, 2] == -1,
                    state.board[0, 0] == state.board[2, 2] && state.board[0, 0] == 1 && state.board[1, 1] == -1,
                    state.board[1, 1] == state.board[2, 2] && state.board[1, 1] == 1 && state.board[0, 0] == -1,
                    state.board[0, 2] == state.board[1, 1] && state.board[0, 2] == 1 && state.board[2, 0] == -1,
                    state.board[0, 2] == state.board[2, 0] && state.board[0, 2] == 1 && state.board[1, 1] == -1,
                    state.board[1, 1] == state.board[2, 0] && state.board[1, 1] == 1 && state.board[0, 2] == -1
                };
                for (int i = 0; i < 3; i++) {
                    if (diag[i]) {
                        engine.postEvent(new GameEvent((2 - i) + "" + (2 - i), "move", code));
                        engine.postEvent(new GameEvent("enter", "action", code));
                        return;
                    }
                }
                for (int i = 3; i < 6; i++) {
                    if (diag[i]) {
                        engine.postEvent(new GameEvent((i - 3) + "" + (5 - i), "move", code));
                        engine.postEvent(new GameEvent("enter", "action", code));
                        return;
                    }
                }
                for (int i = 0; i < 3; i++) {
                    bool[] ver = new bool[] {
                        state.board[i, 0] == state.board[i, 1] && state.board[i, 1] == 0 && state.board[i, 2] == -1,
                        state.board[i, 0] == state.board[i, 2] && state.board[i, 2] == 0 && state.board[i, 1] == -1,
                        state.board[i, 1] == state.board[i, 2] && state.board[i, 2] == 0 && state.board[i, 0] == -1,
                        state.board[0, i] == state.board[1, i] && state.board[1, i] == 0 && state.board[2, i] == -1,
                        state.board[0, i] == state.board[2, i] && state.board[2, i] == 0 && state.board[1, i] == -1,
                        state.board[1, i] == state.board[2, i] && state.board[2, i] == 0 && state.board[0, i] == -1
                    };
                    for (int j = 0; j < 3; j++) {
                        if (ver[j]) {
                            engine.postEvent(new GameEvent((2 - j) + "" + i, "move", code));
                            engine.postEvent(new GameEvent("enter", "action", code));
                            return;
                        }
                    }
                    for (int j = 3; j < 6; j++) {
                        if (ver[j]) {
                            engine.postEvent(new GameEvent(i + "" + (5 - j), "move", code));
                            engine.postEvent(new GameEvent("enter", "action", code));
                            return;
                        }
                    }
                }
                diag = new bool[] {
                    state.board[0, 0] == state.board[1, 1] && state.board[0, 0] == 0 && state.board[2, 2] == -1,
                    state.board[0, 0] == state.board[2, 2] && state.board[0, 0] == 0 && state.board[1, 1] == -1,
                    state.board[1, 1] == state.board[2, 2] && state.board[1, 1] == 0 && state.board[0, 0] == -1,
                    state.board[0, 2] == state.board[1, 1] && state.board[0, 2] == 0 && state.board[2, 0] == -1,
                    state.board[0, 2] == state.board[2, 0] && state.board[0, 2] == 0 && state.board[1, 1] == -1,
                    state.board[1, 1] == state.board[2, 0] && state.board[1, 1] == 0 && state.board[0, 2] == -1
                };
                for (int i = 0; i < 3; i++) {
                    if (diag[i]) {
                        engine.postEvent(new GameEvent((2 - i) + "" + (2 - i), "move", code));
                        engine.postEvent(new GameEvent("enter", "action", code));
                        return;
                    }
                }
                for (int i = 3; i < 6; i++) {
                    if (diag[i]) {
                        engine.postEvent(new GameEvent((i - 3) + "" + (5 - i), "move", code));
                        engine.postEvent(new GameEvent("enter", "action", code));
                        return;
                    }
                }
            }
            List<KeyValuePair<int, int>> points = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if ((engine.getState() as TTTGameState).board[i, j] == -1) {
                        points.Add(new KeyValuePair<int, int>(i, j));
                    }
                }
            }
            KeyValuePair<int, int> point = points[new System.Random().Next(points.Count)];
            int x = point.Value;
            int y = point.Key;
            engine.postEvent(new GameEvent(x + "" + y, "move", code));
            engine.postEvent(new GameEvent("enter", "action", code));
        }
    }
}