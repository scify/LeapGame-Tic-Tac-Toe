using UnityEngine;
using System.Collections;
using System;

public class TTTRule : Rule {

    public delegate bool Applicator(TTTGameState state, GameEvent eve, TTTGameEngine engine);
    public Applicator apllicator;

    public TTTRule(string category, Applicator applier) : base(category) {
        apllicator = applier;
    }

    public override bool applyTo(GameState state, GameEvent eve, GameEngine engine) {
        throw new ArgumentException("Invalid game state type! Expected a TTTGameState, got " + state.GetType().ToString());
    }

    public bool applyTo(TTTGameState state, GameEvent eve, TTTGameEngine engine) {
        return apllicator(state, eve, engine);
    }
	
}