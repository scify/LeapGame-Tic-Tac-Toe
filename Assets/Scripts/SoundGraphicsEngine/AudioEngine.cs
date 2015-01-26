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
	public AudioEngine(string gameName):
	this(-1, gameName) {}



	/**
	 * Constructor of AudioEngine class.
	 * 
	 * This constructor accepts the name of the game and
	 * the index of the player and creates the appropriate
	 * AudioEngine object.
	 * 
	 * @param player - the index of the player (int)
	 * @param gameName - the name of the game (string)
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public AudioEngine(int player, string gameName):
	this(player, gameName, "default") {}



	/**
	 * Constructor of AudioEngine class.
	 * 
	 * This constructor accepts the name of the game, 
	 * the index of the player and the desired game settings
	 * and creates the appropriate AudioEngine object.
	 * 
	 * @param player - the index of the player (int)
	 * @param gameName - the name of the game (string)
	 * @param settingsName - the name of the settings (string)
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public AudioEngine(int player, string gameName, string settingsName) {
		AudioEngine.nOfAudioEngines++;
		this.currentPlayer = player;
		this.audioFilesSettings = new AudioFilesSettings (gameName, settingsName);
	}

    public AudioEngine(int player, string gameName, string menuSettings, string gameSettings) {
        AudioEngine.nOfAudioEngines++;
        this.currentPlayer = player;
        this.audioFilesSettings = new AudioFilesSettings(gameName, menuSettings, gameSettings);
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
	public AudioClip getSoundForPlayer(string theCase, UnityEngine.Vector3 soundOrigin) {
		return this.getSoundForPlayer (theCase, this.currentPlayer, soundOrigin);
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
    public AudioClip getSoundForPlayer(string theCase, UnityEngine.Vector3 playerPosition, UnityEngine.Vector3 soundOrigin) {
		return this.getSoundForPlayer(theCase, this.currentPlayer, this.calculateSoundOrigin(playerPosition, soundOrigin));
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
	public AudioClip getSoundForPlayer(string theCase, int player, UnityEngine.Vector3 playerPosition, UnityEngine.Vector3 soundOrigin) {
		return this.getSoundForPlayer(theCase, player, this.calculateSoundOrigin(playerPosition, soundOrigin));
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
	public AudioClip getSoundForPlayer(string theCase, int player, UnityEngine.Vector3 soundOrigin) {

		if (player.Equals (-1)) {
			if (this.currentPlayer.Equals (-1)) throw new KeyNotFoundException ("Player: " + player + " not found!");
			player = this.currentPlayer;
		}

	
		return (AudioClip) Resources.Load(this.audioFilesSettings.getSoundForPlayer (
			player, theCase, soundOrigin), typeof(AudioClip));
	}



	/**
	 * TODO Add comments
	 */
	public AudioClip getSoundForMenu(string theCase) {
		return (AudioClip) Resources.Load(this.audioFilesSettings.getSoundForMenu(theCase).Replace(".wav", ""), typeof(AudioClip));
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
	 * Gets the total amount of existing audio settings.
	 * 
	 * This public method returns the total amount of audio
	 * settings that exist for the current game according to 
	 * AudioFilesSettings object. 
	 * 
	 * @return int - the total amount of settings
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public int getAmountOfSoundSettings() { 
		return this.audioFilesSettings.getAmountOfSoundSettings ();
	} /* End public int getAmountOfSoundSettings() */
	
	
	
	/**
	 * Gets all sound settings for players
	 * 
	 * This public method returns all sound settings that 
	 * exist for the current game according to 
	 * AudioFilesSettings object.  
	 * 
	 * @return List<string> - the existing players settings
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public List<string> getSettingsAudioForPlayers() {
		return this.audioFilesSettings.getSettingsForPlayers ();
	} /* End public List<string> getAllSoundSettings() */



	/**
	 * Gets all sound settings for menu.
	 * 
	 * This public method returns all sound settings that 
	 * exist for the current game according to 
	 * AudioFilesSettings object.  
	 * 
	 * @return List<string> - the existing settings for menu
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public List<string> getSettingsAudioForMenu() {
		return this.audioFilesSettings.getSettingsForMenu ();
	} /* End public List<string> getSettingsAudioForMenu() */


	
	public void changeSettingsForPlayer(string newSettings) {
		this.audioFilesSettings.changeSettingsForPlayer (newSettings);
	}
	
	

	public void changeSettingsForPlayer(int playerIndex, string newSettings) {
		this.audioFilesSettings.changeSettingsForPlayer (playerIndex, newSettings);
	} 
	
	
	
	public void changeSettingsForMenu (string newSettings) {
		this.audioFilesSettings.changeSettingsForMenu (newSettings);
	}
	
	
	
	/**
	 * Checks if a setting exists for players.
	 * 
	 * This public method checks if a specified setting
	 * exists for the current game according to 
	 * AudioFilesSettings object. Accepts the setting in
	 * question and returns a boolean value representing its
	 * existence. The search is case sensitive. 
	 * 
	 * @param theSetting - the setting in question (string)
	 * @return bool - true if the case exists, false otherwise. 
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public bool isSettingExistsForPlayers(string theSetting) {
		return this.audioFilesSettings.isAudioSettingExistsForPlayer (theSetting);
	} /* End public bool isSettingExistsForPlayers(string theSetting) */



	/**
	 * Checks if a setting exists for menu.
	 * 
	 * This public method checks if a specified setting
	 * exists for the current game according to 
	 * AudioFilesSettings object. Accepts the setting in
	 * question and returns a boolean value representing its
	 * existence. The search is case sensitive. 
	 * 
	 * @param theSetting - the setting in question (string)
	 * @return bool - true if the case exists, false otherwise. 
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public bool isSettingExistsForMenu(string theSetting) {
		return this.audioFilesSettings.isAudioSettingExistsForMenu (theSetting);
	} /* End public bool isSettingExistsForPlayers(string theSetting) */



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