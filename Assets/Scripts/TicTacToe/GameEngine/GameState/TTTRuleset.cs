using UnityEngine;
using System.Collections.Generic;

public class TTTRuleset : Ruleset {

    public TTTRuleset() {
        //load dynamically all rules
        var rules  = (IRule)
        Add(new TTTRule());

        var rule = new TTTRule();
        
    }
	
}