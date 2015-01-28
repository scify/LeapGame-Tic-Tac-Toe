/**
 * The file for the Ruleset class. 
 * 
 * This file holds the definition and implementation 
 * of Ruleset class. This file is part of
 * the LEAP project. 
 * 
 * @file Ruleset.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Nick Zorbas
 * @copyright ??? distributed as is under MIT Licence.
 */
using UnityEngine;
using System.Collections.Generic;

public abstract class Ruleset<T> : List<T> where T : Rule {
    
    public abstract void applyTo(GameState state, GameEvent eve, GameEngine engine);
	
}