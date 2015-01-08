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
	public AudioEngine() {
		AudioEngine.nOfAudioEngines++;
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

	public AudioClip getSound(string sFile, int player, UnityEngine.Vector3 soundOrigin) {



		string theAudioFilePath;

		/*if (AudioEngine.positionUpRight == soundOrigin) {
			theAudioFilePath = this.audioFilesSettings.getPathForSound(player, "UpRight");
		} else if (AudioEngine.positionUpCenter == soundOrigin) {
			theAudioFilePath = this.audioFilesSettings.getPathForSound(player, "UpCenter");
		} else if (AudioEngine.positionUpLeft == soundOrigin) {
			theAudioFilePath = this.audioFilesSettings.getPathForSound(player, "UpLeft");
		} else if (AudioEngine.positionMiddleLeft == soundOrigin) {
			theAudioFilePath = this.audioFilesSettings.getPathForSound(player, "MiddleLeft");
		} else if (AudioEngine.positionMiddleCenter == soundOrigin) {
			theAudioFilePath = this.audioFilesSettings.getPathForSound(player, "MiddleCenter");
		} else if (AudioEngine.positionMiddleRight == soundOrigin) {
			theAudioFilePath = this.audioFilesSettings.getPathForSound(player, "MiddleRight");
		} else if (AudioEngine.positionBottomRight == soundOrigin) {
			theAudioFilePath = this.audioFilesSettings.getPathForSound(player, "BottomLeft");
		} else if (AudioEngine.positionBottomCenter == soundOrigin) {
			theAudioFilePath = this.audioFilesSettings.getPathForSound(player, "BottomCenter");
		} else if (AudioEngine.positionBottomLeft == soundOrigin) {
			theAudioFilePath = this.audioFilesSettings.getPathForSound(player, "BottomRight");
		} else {
			throw new InvalidDataException("Wrong player position");
		}*/



		return null;
	}

	public AudioClip getSoundStream(string sFile, Vector3 player, Vector3 soundOrigin) {
		return null;
	}

	public void updateSoundStreamPosition(AudioClip clipToUpdate, Vector3 player, Vector3 soundOrigin) {
	}

	void setSoundCollection(string newSoundCollection) {
		this.soundCollection = newSoundCollection;
	}

	string getSoundCollection() {
		return this.soundCollection;
	}

	/*!< Amount of existing audioEngines */
	private static int nOfAudioEngines = 0;

	private AudioFilesSettings audioFilesSettings;
	
	/*!< The sounds that will be used in reproduction */
	private string soundCollection; 

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

/* AudioEngine.cs */
/* END OF FILE */