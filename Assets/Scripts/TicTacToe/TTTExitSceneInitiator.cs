using UnityEngine;
using System.Collections.Generic;

public class TTTExitSceneInitiator : MonoBehaviour {

    public float offset_x;
    public float offset_y;

    void Start() {
        TTTStateRenderer renderer = new TTTStateRenderer();
        AudioEngine auEngine = new AudioEngine(0, "Tic-Tac-Toe", Settings.menu_sounds, Settings.game_sounds);

        List<WorldObject> environment = new List<WorldObject>();
        environment.Add(new TTTStaticObject("Prefabs/TTT/Camera_Default", new Vector3(0, 10, 0), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Light_Default", new Vector3(0, 10, 0), false));
        environment.Add(new CanvasObject("Prefabs/TTT/OutroLogo", true, new Vector3(0, 0, 0), false));

        TTTRuleset rules = new TTTRuleset();
        rules.Add(new TTTRule("initialization", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            TTTSoundObject tso = new TTTSoundObject("Prefabs/TTT/AudioSource", auEngine.getSoundForMenu("outro"), Vector3.zero);
            state.environment.Add(tso);
            state.stoppableSounds.Add(tso);
            return false;
        }));

        rules.Add(new TTTRule("soundOver", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            Application.Quit();
            return false;
        }));

        gameObject.AddComponent<TTTMenuEngine>();
        gameObject.AddComponent<TTTMenuUserInterface>();
        gameObject.GetComponent<TTTMenuEngine>().initialize(rules, environment, renderer);
        gameObject.GetComponent<TTTMenuUserInterface>().initialize(gameObject.GetComponent<TTTMenuEngine>());
        gameObject.GetComponent<TTTMenuEngine>().postEvent(new GameEvent("", "initialization", "unity"));
    }
}
