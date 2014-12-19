using UnityEngine;
using System.Collections;

public class TTTRule : Rule {
	
	public TTTRule() {
	}

    public override GameResult applyTo(GameState state) {
        return null;
    }

    public TTTGameResult applyTo(TTTGameState state) {
        return null;
    }
	
}