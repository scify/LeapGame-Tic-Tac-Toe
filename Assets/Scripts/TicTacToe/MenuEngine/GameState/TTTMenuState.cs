
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