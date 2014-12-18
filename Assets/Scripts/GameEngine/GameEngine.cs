using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameEngine : MonoBehaviour {

    public GameState state;
    public StateRenderer renderer;
    public Ruleset rules;
    public Queue<GameEvent> events;

    public abstract void run();

    public abstract void loop();

    public abstract void cleanUp();

    public void postEvent(GameEvent eve) {
        lock (events) {
            events.Enqueue(eve);
        }
    }

}