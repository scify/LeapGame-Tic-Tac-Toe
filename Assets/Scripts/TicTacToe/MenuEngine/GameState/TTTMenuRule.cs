using UnityEngine;
using System.Collections;
using System;

public class TTTMenuRule : Rule {

    public delegate bool Applicator(TTTMenuState state, GameEvent eve, TTTMenuEngine engine);
    public Applicator apllicator;

    public TTTMenuRule(string category, Applicator applier) : base(category) {
        apllicator = applier;
    }

    public override bool applyTo(GameState state, GameEvent eve, GameEngine engine) {
        throw new ArgumentException("Invalid game state type! Expected a TTTMenuGameState, got " + state.GetType().ToString());
    }

    public bool applyTo(TTTMenuState state, GameEvent eve, TTTMenuEngine engine) {
        return apllicator(state, eve, engine);
    }
	
}