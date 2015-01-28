/**
 * The file for the GameState class. 
 * 
 * This file holds the definition and implementation 
 * of GameState class. This file is part of
 * the LEAP project. 
 * 
 * @file GameState.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Nick Zorbas
 * @copyright ??? distributed as is under MIT Licence.
 */
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
    public GameResult result;

	public GameState() {
		timestamp = 0;
	}
}