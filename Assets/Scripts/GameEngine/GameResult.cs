/**
 * The file for the GameResult class. 
 * 
 * This file holds the definition and implementation 
 * of GameResult class. This file is part of
 * the LEAP project. 
 * 
 * @file GameResult.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Nick Zorbas
 * @copyright ??? distributed as is under MIT Licence.
 */
using System;
using System.Collections.Generic;

public abstract class GameResult {

	public GameResult () {
	}
	
	public abstract bool gameOver();

    public abstract int getWinner();
}