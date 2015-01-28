/**
 * The file for the GameEvent class. 
 * 
 * This file holds the definition and implementation 
 * of GameEvent class. This file is part of
 * the LEAP project. 
 * 
 * @file GameEvent.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Nick Zorbas
 * @copyright ??? distributed as is under MIT Licence.
 */
using UnityEngine;
using System.Collections;

public class GameEvent {

    public string payload;
    public string type;
    public string initiator;

	public GameEvent(string payload, string type, string initiator) {
        this.payload = payload;
        this.type = type;
        this.initiator = initiator;
	}
	
}