
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
using System;
using UnityEngine;


public class FontResizer : MonoBehaviour {

    public float targetRes = 2.3f;

	private float lastPixelHeight = -1;
    private TextMesh textMesh;
 
    void Start() {
        textMesh = gameObject.GetComponent<TextMesh>();
        resize();
    }
 
    void Update() {
        if (Camera.main.pixelHeight != lastPixelHeight) resize();
    }
 
    private void resize() {
        float ph = Camera.main.pixelHeight;
        float ch = Camera.main.orthographicSize;
        float pixelRatio = (ch * 0.1f) / ph;
        textMesh.characterSize = pixelRatio * Camera.main.orthographicSize / Math.Max(transform.localScale.x, transform.localScale.y);
        textMesh.fontSize = (int)Math.Round(targetRes / textMesh.characterSize);
        lastPixelHeight = ph;
    }
}
