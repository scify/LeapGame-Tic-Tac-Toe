using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {

    public AudioSource source;
	
	void Update () {
        if (!source.isPlaying) {
            Application.Quit();
        }
	}
}