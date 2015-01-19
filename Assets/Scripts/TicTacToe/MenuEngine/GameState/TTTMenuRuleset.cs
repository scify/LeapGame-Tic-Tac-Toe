using UnityEngine;
using System.Collections.Generic;
using System;

public class TTTMenuRuleset : Ruleset<TTTMenuRule> {

    public override void applyTo(GameState state, GameEvent eve, GameEngine engine) {
        throw new ArgumentException("Invalid game state type! Expected a TTTGameState, got " + state.GetType().ToString(), "state");
    }

    public void applyTo(TTTMenuState state, GameEvent eve, TTTMenuEngine engine) {
        List<TTTMenuRule> rules = this.FindAll(delegate(TTTMenuRule rule) {
            return rule.category.Equals(eve.type) || rule.category.Equals("ALL");
        });
        foreach (TTTMenuRule rule in rules) {
            if (!rule.applyTo(state, eve, engine)) {
                return;
            }
        };
    }

}