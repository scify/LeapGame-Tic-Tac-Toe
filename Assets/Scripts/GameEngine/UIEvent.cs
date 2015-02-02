/**
 * The file for the UIEvent class. 
 * 
 * This file holds the definition and implementation 
 * of UIEvent class. This file is part of
 * the LEAP project. 
 * 
 * @file UIEvent.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Nick Zorbas
 * @copyright ??? distributed as is under MIT Licence.
 */
using UnityEngine;
using System.Collections;

public class UIEvent : GameEvent {
	
    public UIEvent(string payload, string type, string initiator) : base(payload, type, initiator) {

    }

}