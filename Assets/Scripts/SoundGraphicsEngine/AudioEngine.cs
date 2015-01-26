/**
 * The file for the AudioEngine class. 
 * 
 * This file holds the definition and implementation 
 * of AudioEngine class. This file is part of
 * the LEAP project. 
 * 
 * @file AudioEngine.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Konstantinos Drossos
 * @copyright ??? distributed as is under MIT Licence.
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
 * @author Konstantinos Drossos
 */
public class AudioEngine {

	/**
	 * Constructor of AudioEngine class.
	 * 
	 * This constructor accepts the name of the game and
	 * creates the appropriate AudioEngine object. It is
	 * the simpler constructor, accepting only the game's
	 * name. This means that the AudioEngine object will
	 * handle the audio for all players and the audio
	 * settings for both players and menu will be set
	 * to the default ones. 
	 * 
	 * @param gameName - the name of the game (string)
	 * @author Konstantinos Drossos
	 */
	public AudioEngine(string gameName):this(-1, gameName) {}



	/**
	 * Constructor of AudioEngine class.
	 * 
	 * This constructor accepts the name of the game and
	 * the index of the player and creates the appropriate
	 * AudioEngine object. The produced AudioEngine object
	 * will handle the audio for just he specified player. 
	 * Audio settings for both player and menu will be set
	 * to default. 
	 * 
	 * @param player - the index of the player (int)
	 * @param gameName - the name of the game (string)
	 * @author Konstantinos Drossos
	 */
	public AudioEngine(int player, string gameName): this(player, gameName, "default") {}



	/**
	 * Constructor of AudioEngine class.
	 * 
	 * This constructor accepts the name of the game, 
	 * the index of the player and the desired game audio 
	 * settings and creates the appropriate AudioEngine 
	 * object.
	 * 
	 * @param player - the index of the player (int)
	 * @param gameName - the name of the game (string)
	 * @param settingsName - the name of the settings (string)
	 * @author Konstantinos Drossos
	 */
	public AudioEngine(int player, string gameName, string settingsName) {
		AudioEngine.nOfAudioEngines++;
		this.currentPlayer = player;
		this.audioFilesSettings = new AudioFilesSettings (gameName, settingsName);
	}



	/**
	 * Constructor of AudioEngine class.
	 * 
	 * This constructor accepts the name of the game, 
	 * the index of the player and the desired game audio 
	 * settings for both menu and players and creates the
	 * appropriate AudioEngine object.
	 * 
	 * @param player - the index of the player (int)
	 * @param gameName - the name of the game (string)
	 * @param menuSettings - the menu settings (string)
	 * @param settingsName - the name of the settings (string)
	 * @author Konstantinos Drossos
	 */
    public AudioEngine(int player, string gameName, string menuSettings, string gameSettings) {
        AudioEngine.nOfAudioEngines++;
        this.currentPlayer = player;
		this.audioFilesSettings = new AudioFilesSettings(gameName, gameSettings, menuSettings);
    }

	/**
	 * Destructor of AudioEngine.
	 * 
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
	 * @author Konstantinos Drossos
	 */
    public AudioClip getSoundForPlayer(string theCase, UnityEngine.Vector3 playerPosition, 
											UnityEngine.Vector3 soundOrigin) {
		return this.getSoundForPlayer(theCase, this.currentPlayer, 
		                              	this.calculateSoundOrigin(playerPosition, soundOrigin));
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
	 * @author Konstantinos Drossos
	 */
	public AudioClip getSoundForPlayer(string theCase, int player, UnityEngine.Vector3 playerPosition, 
	                                   	UnityEngine.Vector3 soundOrigin) {
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
	 * @throw KeyNotFoundException - thrown when player is not found or not appropriate settings for player
	 * @author Konstantinos Drossos
	 */
	public AudioClip getSoundForPlayer(string theCase, int player, UnityEngine.Vector3 soundOrigin) {

		if (player == -1) {
			if (this.currentPlayer == -1) throw new KeyNotFoundException ("Player: " + player + " not found!");
			player = this.currentPlayer;
		}

		return (AudioClip) Resources.Load(this.audioFilesSettings.getSoundForPlayer (
			player, theCase, soundOrigin), typeof(AudioClip));
	}



	/**
	 * Gets the path of sound for menu.
	 * 
	 * This public method returns the path for
	 * the sound that corresponds to the specified
	 * case. Matching of cases and sound paths is
	 * defined in the appropriate XML file with the
	 * sound settings. 
	 * 
	 * @param theCase - the specified case for which the sound is sought (string)
	 * @return AudioClip - the AudioClip object containing the sound
	 * @author Konstantinos Drossos
	 */
	public AudioClip getSoundForMenu(string theCase) {
		return (AudioClip) Resources.Load(this.audioFilesSettings.getSoundForMenu(theCase).Replace(".wav", ""), 
		                                  	typeof(AudioClip));
	}



	/**
	 * Method will be implemented in future versions.
	 */
	public AudioClip getSoundStream(string sFile, Vector3 player, Vector3 soundOrigin) {
		return null;
	}



	/**
	 * Method will be implemented in future versions.
	 */
	public void updateSoundStreamPosition(AudioClip clipToUpdate, Vector3 player, Vector3 soundOrigin) {
	}



	/**
	 * Getter for amount of audio engines.
	 * 
	 * This public method returns the amount
	 * of created AudioEngine objects. 
	 * 
	 * @return int - the amount of created AudioEngine objects
	 * @author Konstantinos Drossos
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
	 * @author Konstantinos Drossos
	 */
	public int getAmountOfSoundSettings() { 
		return this.audioFilesSettings.getAmountOfSoundSettings ();
	}
	
	
	
	/**
	 * Gets all sound settings for players
	 * 
	 * This public method returns all sound settings that 
	 * exist for the current game according to 
	 * AudioFilesSettings object.  
	 * 
	 * @return List<string> - the existing players settings
	 * @author Konstantinos Drossos
	 */
	public List<string> getSettingsAudioForPlayers() {
		return this.audioFilesSettings.getSettingsForPlayers ();
	}



	/**
	 * Gets all sound settings for menu.
	 * 
	 * This public method returns all sound settings that 
	 * exist for the current game according to 
	 * AudioFilesSettings object.  
	 * 
	 * @return List<string> - the existing settings for menu
	 * @author Konstantinos Drossos
	 */
	public List<string> getSettingsAudioForMenu() {
		return this.audioFilesSettings.getSettingsForMenu ();
	}



	/**
	 * Changes the current audio setting for players.
	 * 
	 * This public method allows the on-the-fly changing 
	 * of audio settings for players. It accepts a string 
	 * which should be a valid settings name, contained in
	 * the game's settings XML document. 
	 * 
	 * @param newSettings - the name of the new settings (string)
	 * @author Konstantinos Drossos
	 */
	public void changeSettingsForPlayer(string newSettings) {
		this.audioFilesSettings.changeSettingsForPlayer (newSettings);
	}
	
	

	/**
	 * Changes the current audio setting for players.
	 * 
	 * This public method allows the on-the-fly changing 
	 * of audio settings for a cpecified player. It accepts 
	 * a string which should be a valid settings name 
	 * contained in the game's settings XML document, and
	 * the player's index. Use this method only if more
	 * than one player is specified at the initialization
	 * of the AudioEngine object. 
	 * 
	 * @param playerIndex - the player's index
	 * @param newSettings - the name of the new settings (string)
	 * @author Konstantinos Drossos
	 */
	public void changeSettingsForPlayer(int playerIndex, string newSettings) {
		this.audioFilesSettings.changeSettingsForPlayer (playerIndex, newSettings);
	} 
	
	

	/**
	 * Changes the existing settings for menu sounds. 
	 * 
	 * This public method accepts a valid settings name
	 * and changes the current audio settings for menu
	 * sounds. The valid menu sounds' settings names are
	 * declared at the appropriate XML document with the
	 * audio settings. 
	 * 
	 * @param newSetting - the name of the new settings (string)
	 * @author Konstantinos Drossos
	 */
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
	 * @author Konstantinos Drossos
	 */
	public bool isSettingExistsForPlayers(string theSetting) {
		return this.audioFilesSettings.isAudioSettingExistsForPlayer (theSetting);
	}



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
	 * @author Konstantinos Drossos
	 */
	public bool isSettingExistsForMenu(string theSetting) {
		return this.audioFilesSettings.isAudioSettingExistsForMenu (theSetting);
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
	 * @author Konstantinos Drossos
	 */
	private UnityEngine.Vector3 calculateSoundOrigin(UnityEngine.Vector3 playerPosition,
	                                                 	UnityEngine.Vector3 soundOrigin) {
		return new UnityEngine.Vector3 ();
	}

	private int currentPlayer; /*!< The inde of the current player */
	private AudioFilesSettings audioFilesSettings; /*!< The audio settings handler */
	private static int nOfAudioEngines = 0; /*!< Amount of existing audioEngines */
}

/* Scripts/SoundGraphicsEngine/AudioEngine.cs */
/* EOF */
