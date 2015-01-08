using UnityEngine;
using System.Collections;

public class HumanPlayer : Player {
	
    public UserInterface ui;

    public HumanPlayer(string code, string name, UserInterface ui) : base(code, name) {
        this.ui = ui;
	}

}