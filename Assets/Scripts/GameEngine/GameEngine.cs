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