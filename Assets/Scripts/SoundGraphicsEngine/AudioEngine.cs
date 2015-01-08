using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/**
 * Class to handle the audio reproduction
 */
public class AudioEngine {



	/**
	 * Getter for amount of audio engines
	 */
	public static int getNOfAudioEngines () {
		return AudioEngine.nOfAudioEngines;
	}

	/**
	 * Constructor
	 */
	public AudioEngine(string gameName):this(0, gameName) {}

	public AudioEngine(int player, string gameName) {
		AudioEngine.nOfAudioEngines++;
		this.currentPlayer = player;
		this.audioFilesSettings = new AudioFilesSettings (gameName);
	}

	/**
	 * Destructor of AudioEngine
	 */
	~AudioEngine() {
		AudioEngine.nOfAudioEngines--;
	}


	/**
	 * Get the sound for a position
	 */
	public AudioClip getSound() {
		return null;
	}

	public AudioClip getSound(string theCase, UnityEngine.Vector3 soundOrigin) {
		return this.getSound (theCase, this.currentPlayer, soundOrigin);
	}

    public AudioClip getSound(string theCase, UnityEngine.Vector3 playerPosition, UnityEngine.Vector3 soundOrigin) {
		return this.getSound(theCase, this.currentPlayer, this.calculateSoundOrigin(playerPosition, soundOrigin));
	}

	public AudioClip getSound(string theCase, int player, UnityEngine.Vector3 soundOrigin) {

		if (player.Equals (0)) {
			if (this.currentPlayer.Equals (0)) throw new KeyNotFoundException ("Player: " + player + " not found!");
			player = this.currentPlayer;
		}

		return (AudioClip) Resources.Load(this.audioFilesSettings.getSoundForPlayer (player, theCase, soundOrigin), typeof(AudioClip));
	}


	public AudioClip getSoundStream(string sFile, Vector3 player, Vector3 soundOrigin) {
		return null;
	}

	public void updateSoundStreamPosition(AudioClip clipToUpdate, Vector3 player, Vector3 soundOrigin) {
	}

	private UnityEngine.Vector3 calculateSoundOrigin(UnityEngine.Vector3 playerPosition, UnityEngine.Vector3 soundOrigin) {
		return new UnityEngine.Vector3 ();
	}

	/*!< Amount of existing audioEngines */
	private static int nOfAudioEngines = 0;

	private AudioFilesSettings audioFilesSettings;
	
	private int currentPlayer; 

	private static readonly Vector3 positionUpRight = Vector3.up + Vector3.right;
	private static readonly Vector3 positionUpCenter = Vector3.up;
	private static readonly Vector3 positionUpLeft = Vector3.up + Vector3.left;
	private static readonly Vector3 positionMiddleLeft = Vector3.left;
	private static readonly Vector3 positionMiddleCenter = Vector3.zero;
	private static readonly Vector3 positionMiddleRight = Vector3.right;
	private static readonly Vector3 positionBottomLeft = Vector3.down + Vector3.left;
	private static readonly Vector3 positionBottomCenter = Vector3.down;
	private static readonly Vector3 positionBottomRight = Vector3.down + Vector3.right;

}

/* Scripts/SoundGraphicsEngine/AudioEngine.cs */
/* END OF FILE */
