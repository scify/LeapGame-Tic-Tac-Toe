using UnityEngine;
using System.Collections.Generic;
using System;

public class TTTRuleset : Ruleset<TTTRule> {

    public override void applyTo(GameState state, GameEvent eve, GameEngine engine) {
        throw new ArgumentException("Invalid game state type! Expected a TTTGameState, got " + state.GetType().ToString(), "state");
    }

    public void applyTo(TTTGameState state, GameEvent eve, TTTGameEngine engine) {
        List<TTTRule> rules = this.FindAll(delegate(TTTRule rule) {
            return rule.category.Equals(eve.type) || rule.category.Equals("ALL");
        });
        foreach (TTTRule rule in rules) {
            if (!rule.applyTo(state, eve, engine)) {
                return;
            }
        };
    }

}