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
