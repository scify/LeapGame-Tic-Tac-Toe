using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TTTGameEngine : GameEngine {

	public void Start() {
        run();
	}

    public override void run() {
        state = new TTTGameState();
        renderer = new TTTStateRenderer();
        rules = new TTTRuleset();
        events = new Queue<GameEvent>();
    }

    public void Update() {
        loop();
    }

    public override void loop() {

	}

    public override void cleanUp() {
        Application.LoadLevel(Application.loadedLevel);
	}

}