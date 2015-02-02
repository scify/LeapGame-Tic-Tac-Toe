/**
 * The file for the GameEngine class. 
 * 
 * This file holds the definition and implementation 
 * of GameEngine class. This file is part of
 * the LEAP project. 
 * 
 * @file GameEngine.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Nick Zorbas
 * @copyright ??? distributed as is under MIT Licence.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameEngine : MonoBehaviour {

    public abstract void run();

    public abstract void loop();

    public abstract void cleanUp();

    public abstract void postEvent(GameEvent eve);

    public abstract GameState getState();

    public abstract StateRenderer getRenderer();
}