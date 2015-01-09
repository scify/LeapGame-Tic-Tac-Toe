/**
 * File holding the definition of AudioEngine class. 
 * 
 * @file AudioEngine.cs
 * @author Konstantinos Drossos
 */
using UnityEngine;
using System.Collections.Generic;
using System.IO;

/**
 * Class to handle the audio reproduction.
 * 
 * This class is responsible for handling the creation
 * of used audio clips. It uses the AudioFilesSettings 
 * class in order to get the appropriate files and creates
 * the appropriate AudioClip objects. 
 * 
 * @access Public
 * @author Konstantinos Drossos
 */
public class AudioEngine {

	/**
	 * Constructor of AudioEngine class.
	 * 
	 * This constructor accepts the name of the game and
	 * creates the appropriate AudioEngine object.
	 * 
	 * @param gameName - the name of the game (string)
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public AudioEngine(string gameName):this(-1, gameName) {}



	/**
	 * Constructor of AudioEngine class.
	 * 
	 * This constructor accepts the name of the game and
	 * the index of the player and creates the appropriate
	 * AudioEngine object.
	 * 
	 * @param gameName - the name of the game (string)
	 * @param player - the index of the player (int)
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public AudioEngine(int player, string gameName) {
		AudioEngine.nOfAudioEngines++;
		this.currentPlayer = player;
		this.audioFilesSettings = new AudioFilesSettings (gameName);
	}



	/**
	 * Destructor of AudioEngine.
	 * 
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	~AudioEngine() {
		AudioEngine.nOfAudioEngines--;
	}



	/**
	 * Gets the appropriate AudioClip object.
	 * 
	 * This public method accepts the case and the origin of sound
	 * and returns the appropriate AudioClip object. As player is
	 * considered the one defined at the creation of AudioEngine object. 
	 * 
	 * @param theCase - the case for sound as defined in the audio settings (string)
	 * @param soundOrigin - the origin of the sound (UnityEngine.Vector3)
	 * @return AudioClip - the audio clip with the appropriate sound
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public AudioClip getSound(string theCase, UnityEngine.Vector3 soundOrigin) {
		return this.getSound (theCase, this.currentPlayer, soundOrigin);
	}



	/**
	 * Gets the appropriate AudioClip object.
	 * 
	 * This public method accepts the case, the player's position and
	 * the origin of sound and returns the appropriate AudioClip object. 
	 * As player is considered the one defined at the creation of AudioEngine 
	 * object. 
	 * 
	 * @param theCase - the case for sound as defined in the audio settings (string)
	 * @param playerPosition - the player's position (UnityEngine.Vector3) 
	 * @param soundOrigin - the origin of the sound (UnityEngine.Vector3)
	 * @return AudioClip - the audio clip with the appropriate sound
	 * @access Public
	 * @author Konstantinos Drossos
	 */
    public AudioClip getSound(string theCase, UnityEngine.Vector3 playerPosition, UnityEngine.Vector3 soundOrigin) {
		return this.getSound(theCase, this.currentPlayer, this.calculateSoundOrigin(playerPosition, soundOrigin));
	}



	/**
	 * Gets the appropriate AudioClip object.
	 * 
	 * This public method accepts the case, the player's position, the player's 
	 * position and the origin of sound and returns the appropriate AudioClip object. 
	 * As player is considered the one defined at the creation of AudioEngine 
	 * object. 
	 * 
	 * @param theCase - the case for sound as defined in the audio settings (string)
	 * @param player - the player's index (int)
	 * @param playerPosition - the player's position (UnityEngine.Vector3) 
	 * @param soundOrigin - the origin of the sound (UnityEngine.Vector3)
	 * @return AudioClip - the audio clip with the appropriate sound
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public AudioClip getSound(string theCase, int player, UnityEngine.Vector3 playerPosition, UnityEngine.Vector3 soundOrigin) {
		return this.getSound(theCase, player, this.calculateSoundOrigin(playerPosition, soundOrigin));
	}


	
	/**
	 * Gets the appropriate AudioClip object.
	 * 
	 * This public method accepts the case, the player's index 
	 * and the origin of sound and returns the appropriate AudioClip
	 * object. As player is considered the one defined at the creation 
	 * of AudioEngine object. 
	 * 
	 * @param theCase - the case for sound as defined in the audio settings (string)
	 * @param player - the player's index (int) 
	 * @param soundOrigin - the origin of the sound (UnityEngine.Vector3)
	 * @return AudioClip - the audio clip with the appropriate sound
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public AudioClip getSound(string theCase, int player, UnityEngine.Vector3 soundOrigin) {

		if (player.Equals (-1)) {
			if (this.currentPlayer.Equals (-1)) throw new KeyNotFoundException ("Player: " + player + " not found!");
			player = this.currentPlayer;
		}

		return (AudioClip) Resources.Load(this.audioFilesSettings.getSoundForPlayer (player, theCase, soundOrigin), typeof(AudioClip));
	}



	public AudioClip getSoundStream(string sFile, Vector3 player, Vector3 soundOrigin) {
		return null;
	}



	public void updateSoundStreamPosition(AudioClip clipToUpdate, Vector3 player, Vector3 soundOrigin) {
	}



	/**
	 * Getter for amount of audio engines
	 */
	public static int getNOfAudioEngines () {
		return AudioEngine.nOfAudioEngines;
	}



	/**
	 * Calculates the origin of sound.
	 * 
	 * Ths private method calculates the appropriate origin
	 * of sound according to the player's position.
	 * 
	 * @param playerPosition - the player's position (UnityEngine.Vector3)
	 * @param soundOrigin - the sound original origin (UnityEngine.Vector3)
	 * @return UnityEngine.Vector3 - the appropriate sound origin
	 * @access Private
	 * @author Konstantinos Drossos
	 */
	private UnityEngine.Vector3 calculateSoundOrigin(UnityEngine.Vector3 playerPosition, UnityEngine.Vector3 soundOrigin) {
		return new UnityEngine.Vector3 ();
	}



	/* ==============  Arguments section ==============  */

	private int currentPlayer; /*!< The inde of the current player */
	private AudioFilesSettings audioFilesSettings; /*!< The audio settings handler */


	private static int nOfAudioEngines = 0; /*!< Amount of existing audioEngines */
}

/* Scripts/SoundGraphicsEngine/AudioEngine.cs */
/* END OF FILE */