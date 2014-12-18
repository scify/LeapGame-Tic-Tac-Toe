using System;

public abstract class TTTGameResult {

    public enum GameStatus {
        Ongoing = 0,
        Invalid = 1,
        Won = 2,
        Draw = 3
    }

    private GameStatus status;
    private int winner;

    public TTTGameResult(GameStatus status, int winner) {
        this.status = status;
        this.winner = winner;
	}

    public bool gameOver() {
        return status == GameStatus.Draw || status == GameStatus.Won;
    }

    public bool isValid() {
        return status != GameStatus.Invalid;
    }

    public int getWinner() {
        return winner;
    }
}