using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class GameState {

	public long timestamp;
	public List<Actor> actors;
	public List<Player> players;
	public List<WorldObject> environment;
    public int curPlayer;

	public GameState() {
		timestamp = 0;
	}
}