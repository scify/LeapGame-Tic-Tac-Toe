using UnityEngine;
using System.Collections;
using System;

public class TTTRule : Rule {

    public delegate bool Applicator(TTTGameState state, GameEvent eve, TTTGameEngine engine);
    public delegate bool MenuApplicator(TTTMenuState state, GameEvent eve, TTTMenuEngine engine);
    public Applicator apllicator;
    public MenuApplicator menuApllicator;

    public TTTRule(string category, Applicator applier) : base(category) {
        apllicator = applier;
    }
    public TTTRule(string category, MenuApplicator applier)
        : base(category) {
            menuApllicator = applier;
    }

    public override bool applyTo(GameState state, GameEvent eve, GameEngine engine) {
        throw new ArgumentException("Invalid game state type! Expected a TTTGameState, got " + state.GetType().ToString());
    }

    public bool applyTo(TTTGameState state, GameEvent eve, TTTGameEngine engine) {
        return apllicator(state, eve, engine);
    }
    public bool applyTo(TTTMenuState state, GameEvent eve, TTTMenuEngine engine) {
        return menuApllicator(state, eve, engine);
    }
	
}