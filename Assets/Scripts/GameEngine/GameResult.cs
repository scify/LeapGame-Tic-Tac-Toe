using System;

public abstract class GameResult {

	public GameResult () {
	}
	
	public abstract bool gameOver();

    public abstract bool isValid();
}