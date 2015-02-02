/**
 * The file for the Rule class. 
 * 
 * This file holds the definition and implementation 
 * of Rule class. This file is part of
 * the LEAP project. 
 * 
 * @file Rule.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Nick Zorbas
 * @copyright ??? distributed as is under MIT Licence.
 */
using UnityEngine;
using System.Collections;

public abstract class Rule {

    public string category;

    public Rule(string category) {
        this.category = category;
    }

	public abstract bool applyTo(GameState state, GameEvent eve, GameEngine engine);
	
}