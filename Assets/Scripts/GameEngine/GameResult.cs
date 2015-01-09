using System;
using System.Collections.Generic;

public abstract class GameResult {

	public GameResult () {
	}
	
	public abstract bool gameOver();

    public abstract int getWinner();
}