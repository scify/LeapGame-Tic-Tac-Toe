using System;
using UnityEngine;

class TextCanvasObject : CanvasObject {
    public string text;

    public TextCanvasObject(string prefab, bool coversCamera, string text, Vector3 position, bool hidden) : base(prefab, coversCamera, position, hidden) {
        this.text = text;
	}
}