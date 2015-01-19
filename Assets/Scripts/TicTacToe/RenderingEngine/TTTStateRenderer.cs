using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;

public class TTTStateRenderer : StateRenderer {

	public TTTStateRenderer() {
	}

    public override void render(GameState state) {
        throw new System.NotImplementedException("This engine can not be used for this game");
    }

    public void render(TTTMenuState state) {
        List<WorldObject> currentRenderedObjects = new List<WorldObject>();
        render(state.environment, currentRenderedObjects);
        List<WorldObject> toRemove = new List<WorldObject>();
        foreach (WorldObject so in rendered.Keys) {
            if (!currentRenderedObjects.Contains(so)) {
                UnityEngine.Object.Destroy(rendered[so]);
                toRemove.Add(so);
            }
        }
        foreach (WorldObject so in toRemove) {
            rendered.Remove(so);
            if (so is TTTSoundObject) {
                state.stoppableSounds.Remove(so as TTTSoundObject);
            }
        }
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
            if (so is TTTSoundObject) {
                state.blockingSounds.Remove(so as TTTSoundObject);
            }
        }
    }

    private void render<T>(List<T> set, List<WorldObject> currentRenderedObjects) where T : WorldObject {
        List<T> toRemove = new List<T>();
        foreach (T so in set) {
            if (so is IUnityRenderable) {
                if (!rendered.ContainsKey(so)) {
                    GameObject go = (GameObject) PrefabUtility.InstantiatePrefab(Resources.Load((so as IUnityRenderable).getPrefab()));
                    go.transform.position = so.position;
                    rendered.Add(so, go);
                    if (so is TTTMenuItem) {
                        go.GetComponentInChildren<TextMesh>().text = (so as TTTMenuItem).message;
                    }
                    if (so is TTTSoundObject) {
                        go.GetComponent<AudioSource>().clip = (so as TTTSoundObject).clip;
                        go.GetComponent<AudioSource>().Play();
                    }
                } else {
                    string prefabPath = "Assets/Resources/" + (so as IUnityRenderable).getPrefab() + ".prefab";
                    if (!prefabPath.Equals(AssetDatabase.GetAssetPath(PrefabUtility.GetPrefabParent(rendered[so])))) {
                        UnityEngine.Object.Destroy(rendered[so]);
                        rendered.Remove(so);
                        GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load((so as IUnityRenderable).getPrefab()));
                        rendered.Add(so, go);
                    }
                    if (so is TTTSoundObject) {
                        if (!rendered[so].GetComponent<AudioSource>().isPlaying) {
                            toRemove.Add(so);
                            continue;
                        }
                    }
                    if (so is TTTMenuItem) {
                        rendered[so].GetComponentInChildren<TextMesh>().text = (so as TTTMenuItem).message;
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