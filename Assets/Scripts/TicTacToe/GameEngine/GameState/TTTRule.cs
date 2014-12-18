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




public interface IRule {

    TTTGameResult applyTo(TTTGameState state);
}


public class Rule1 : IRule {
    
    public Rule1(string x) {

    }
    public TTTGameResult applyTo(TTTGameState state) {
        throw new System.NotImplementedException();
    }
}

public class Rule2 : IRule { 
}