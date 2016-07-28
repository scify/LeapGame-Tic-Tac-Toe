
/**
 * Copyright 2016 , SciFY NPO - http://www.scify.org
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using UnityEngine;
using System.Collections;

public class TTTActor : Actor, IUnityRenderable {

    public string code;
    public string prefab;
    public Interaction interaction;

    public delegate void Interaction(WorldObject target, GameEngine engine);

    public TTTActor(string code, string prefab, Vector3 position, bool hidden, Interaction interaction) : base(position, hidden) {
        this.prefab = prefab;
        this.code = code;
        this.interaction = interaction;
	}

    public string getPrefab() {
        return prefab;
    }

    public override void interact(WorldObject target, GameEngine engine) {
        interaction(target, engine);
    }

}