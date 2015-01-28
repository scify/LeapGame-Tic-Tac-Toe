/**
 * The file for the StaticObject class. 
 * 
 * This file holds the definition and implementation 
 * of StaticObject class. This file is part of
 * the LEAP project. 
 * 
 * @file StaticObject.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Nick Zorbas
 * @copyright ??? distributed as is under MIT Licence.
 */
using UnityEngine;
using System.Collections;

public class StaticObject : WorldObject {

    public StaticObject(Vector3 position, bool hidden) : base(position, hidden) {
	}

}