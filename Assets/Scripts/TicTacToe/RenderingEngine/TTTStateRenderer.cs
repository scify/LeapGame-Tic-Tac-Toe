using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class TTTStateRenderer : StateRenderer {

	public TTTStateRenderer() {
	}

    public override void render(GameState state) {
        throw new System.NotImplementedException("This engine can not be used for this game");
    }

    public void render(TTTGameState state) {
        List<WorldObject> currentRenderedObjects = new List<WorldObject>();
        render(state.environment, currentRenderedObjects);
        render(state.actors, currentRenderedObjects);
        List<WorldObject> toRemove = new List<WorldObject>();
        foreach (WorldObject so in rendered.Keys) {
            if (!currentRenderedObjects.Contains(so)) {
                UnityEngine.Object.Destroy(rendered[so]);
                toRemove.Add(so);
            }
        }
        foreach (WorldObject so in toRemove) {
            rendered.Remove(so);
        }
    }

    private void render<T>(List<T> set, List<WorldObject> currentRenderedObjects) where T : WorldObject {
        List<T> toRemove = new List<T>();
        foreach (T so in set) {
            if (so is IUnityRenderable) {
                if (!rendered.ContainsKey(so)) {
                    GameObject go = (GameObject)Object.Instantiate(Resources.Load((so as IUnityRenderable).getPrefab()));
                    go.transform.position = so.position;
                    rendered.Add(so, go);
                    if (so is TTTSoundObject) {
                        go.GetComponent<AudioSource>().clip = (so as TTTSoundObject).clip;
                        go.GetComponent<AudioSource>().Play();
                    }
                } else {
                    if (so is TTTSoundObject) {
                        if (!rendered[so].GetComponent<AudioSource>().isPlaying) {
                            toRemove.Add(so);
                            continue;
                        }
                    }
                    rendered[so].transform.position = so.position;
                }
                currentRenderedObjects.Add(so);
            }
        }
        foreach (T so in toRemove) {
            set.Remove(so);
        }
    }
}