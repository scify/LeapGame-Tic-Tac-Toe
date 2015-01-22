/**
 * This file contains the AudioFilesSettings class. 
 * 
 * This class is responsible for loading and handling
 * the audio settings. Audio settings contain the amount
 * of players that the game can hold, the cases at which
 * sound will be reproduced, the corresponding positions of
 * sound reproduction and the relative (to the application)
 * paths of the sounds. 
 * 
 * //TODO Add legal stuff.... (have to? )
 * 
 * @file AudioFilesSettings.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Konstantinos Drossos
 * @copyright ??? distributed as is under MIT Licence.
 */ 

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using UnityEngine;

/**
 * The audio file settings for players.
 * 
 * This class handles the audio file settings for
 * all players in a specified game. Public methods are:
 * 
 *   - AudioFilesSettings (string )
 *   - string getSoundForPlayer (int , string , UnityEngine.Vector3 )
 *   - string getMenuSound(string )
 *   - int getAmountOfSoundSettings()
 *   - List<string> getAllSoundSettings()
 *   - bool isSettingExists(string theCase)
 * 
 * @access Public
 * @author Konstantinos Drossos
 */
public class AudioFilesSettings {

	/** 
	 * Constructor of the AudioFilesSettings class.
	 * 
	 * The constructor of the AudioFilesSettings class accepts
	 * the game name and sets up the audio files' settings for the
	 * specified game. 
	 * 
	 * @param gameName - the name of the game to be played (string)
	 * @throw Exception - rethrows all catched exceptions after print their message to debug log
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public AudioFilesSettings (string gameName):
	this(gameName, "default", "default"){}

	public AudioFilesSettings(string gameName, string playersSettings):
	this(gameName, "default", playersSettings){}
	
	public AudioFilesSettings(string gameName, string menuSetting, string playerSetting) {

		AudioXMLDocument gameSettings = new AudioXMLDocument() ;

		try { 
			//gameSettings.Load(gameName); 
			gameSettings.LoadSettingsXML(gameName); 
		} catch (Exception e) { Debug.Log (e.Message); throw e; }

		this.initialiseLists ();

		try { 
			this.nOfPlayers_max = gameSettings.MaximumPlayers;
			this.nOfPlayers_min = gameSettings.MinimumPlayers;
			this.audioSettingsForPlayers = gameSettings.getAudioSettingsOf ("players");
			this.audioSettingsForMenu = gameSettings.getAudioSettingsOf ("menu");
		} catch (Exception e) { Debug.Log (e.Message); throw e; }

		this.currentAudioSettingForMenu = menuSetting;
		this.currentAudioSettingForPlayer = playerSetting;

		try {
			foreach (int playerIndx in Enumerable.Range(0, this.nOfPlayers_max - 1) ) {
				this.audioFilesForPlayer.Add ( gameSettings.getFilesForPlayer(playerIndx, playerSetting) );
			}

			this.audioFilesForMenu = gameSettings.getFilesForMenu(menuSetting);
		} catch (Exception e) { Debug.Log (e.Message); throw e; }

		this.gameName = gameName;

	} /* End public AudioFilesSettings(string gameName, string menuSetting, string playerSetting) */
	


	/**
	 * Gets the path of a sound for a given player and position.
	 * 
	 * This public method accepts the player's index, the case and the sound origin
	 * and returns the path of the appropriate sound.
	 * 
	 * @param player - the index of the player (int)
	 * @param theCase - the case of the sound (string)
	 * @param soundOrigin - the origin of the sound (UnityEngine.Vector3)
	 * @return string - the path of the sound
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public string getSoundForPlayer (int player, string theCase, UnityEngine.Vector3 soundOrigin) {
		return this.audioFilesForPlayer [player].Find ( delegate (AudioFileForGame af) {
			if ( af.TheCase.Equals (theCase) && af.ThePosition.Equals (soundOrigin) ) return true; else return false; 
		}).ThePath;
	} /* End public string getSoundForPlayer (int player, string theCase, UnityEngine.Vector3 soundOrigin) */



	/**
	 * Returns the path of a sound for the game menu.
	 * 
	 * This public method returns the appropriate path for a sound
	 * of the menu. The sound is determined by its case. 
	 * 
	 * @param theCase - the menu case (string)
	 * @return string - the path of the sound
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public string getSoundForMenu (string theCase) {
		return this.audioFilesForMenu.Find ( delegate (AudioFileForGame af) { 
			if ( af.TheCase.Equals (theCase) ) return true; else return false; 
		}).ThePath;
	}



	/**
	 * Changes the current settings for all players.
	 * 
	 * This public method changes the current sound settings for all players.
	 * 
	 * @param newSettings - the name of the new settings (string)
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public void changeSettingsForPlayer(string newSettings) {

		foreach (int playerIndx in Enumerable.Range(0, this.nOfPlayers_max - 1) ) 
			this.changeSettingsForPlayer(playerIndx, newSettings);

	} /* End public void changePlayerSettings(int playerIndex, string newSettings) */



	/**
	 * Changes the current settings for a player.
	 * 
	 * This public method changes the current sound settings
	 * a specified player.
	 * 
	 * @param playerIndex - the index of the specified player (int)
	 * @param newSettings - the name of the new settings (string)
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public void changeSettingsForPlayer(int playerIndex, string newSettings) {

		AudioXMLDocument gameSettings = new AudioXMLDocument() ;
		//gameSettings.Load (this.gameName);
		gameSettings.LoadSettingsXML (this.gameName);

		this.audioFilesForPlayer [playerIndex].Clear ();
		this.audioFilesForPlayer [playerIndex] = gameSettings.getFilesForPlayer (playerIndex, newSettings);
		this.currentAudioSettingForPlayer = newSettings;
	} /* End public void changePlayerSettings(int playerIndex, string newSettings) */



	/**
	 * Changes the current settings for menu.
	 * 
	 * This public method changes the current sound settings 
	 * for menu.
	 * 
	 * @param newSettings - the name of the new settings (string)
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public void changeSettingsForMenu (string newSettings) {
		AudioXMLDocument gameSettings = new AudioXMLDocument() ;
		//gameSettings.Load (this.gameName);
		gameSettings.LoadSettingsXML (this.gameName);

		this.audioFilesForMenu = gameSettings.getFilesForMenu(newSettings);
		this.currentAudioSettingForMenu = newSettings;
	}



	/**
	 * The current audio settings for the menu.
	 * 
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public string CurrentAudioSettingForMenu {
		get { return this.currentAudioSettingForMenu; }
	}



	/**
	 * The current audio settings for the player(s).
	 * 
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public string CurrentAudioSettingForPlayer {
		get { return currentAudioSettingForPlayer; }
	}

	

	/**
	 * Gets the total amount of existing audio settings.
	 * 
	 * This public method returns the total amount of audio
	 * settings that exist for the current game. If the variable
	 * holding the settings is not initialised, the method throws
	 * MemberAccessException. 
	 * 
	 * @return int - the total amount of settings
	 * @throw MemberAccessException - if the appropriate variable is not initialised
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public int getAmountOfSoundSettings() { 
		if (this.audioSettingsForMenu == null) throw new MemberAccessException("Uninitialised variable");
		return this.audioSettingsForMenu.Count; 
	} /* End public int getAmountOfSoundSettings() */



	/**
	 * Gets all sound settings for player(s).
	 * 
	 * This public method returns all sound settings that 
	 * exist for the current game. 
	 * 
	 * @return List<string> - the existing settings
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public List<string> getSettingsForPlayers () {
		return this.audioSettingsForPlayers;
	} /* End public List<string> getSettingsForPlayers () */



	/**
	 * Gets all sound settings for menu.
	 * 
	 * This public method returns all sound settings that 
	 * exist for the current game. 
	 * 
	 * @return List<string> - the existing settings
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public List<string> getSettingsForMenu () {
		return this.audioSettingsForMenu;
	} /* End public List<string> getSettingsForMenu () */



	/**
	 * Checks if an audio menu setting exists.
	 * 
	 * This public method checks if a specified setting
	 * exists for the current game. Accepts the setting in
	 * question and returns a boolean value representing its
	 * existence. The search is case sensitive. 
	 * 
	 * @param theSetting - the case in question (string)
	 * @return bool - true if the case exists, false otherwise. 
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public bool isAudioSettingExistsForMenu(string theSetting) {
		return this.audioSettingsForMenu.Exists (c => c.Equals (theSetting));
	} /* End public bool isAudioMenuSettingExists(string theCase) */



	/**
	 * Checks if an audio for player setting exists.
	 * 
	 * This public method checks if a specified setting
	 * exists for the current game. Accepts the setting in
	 * question and returns a boolean value representing its
	 * existence. The search is case sensitive. 
	 * 
	 * @param theSetting - the case in question (string)
	 * @return bool - true if the case exists, false otherwise. 
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public bool isAudioSettingExistsForPlayer(string theSetting) {
		return this.audioSettingsForPlayers.Exists (c => c.Equals (theSetting));
	} /* End public bool isAudioPlayerSettingExists(string theCase) */
	
	
	
	/**
	 * Initialises the lists for the current object.
	 * 
	 * This private method initiliased all lists that will be used 
	 * by the current object. 
	 * 
	 * @access Private
	 * @author Konstantinos Drossos
	 */
	private void initialiseLists () {
		this.audioFilesForMenu = new List<AudioFileForGame> ();
		this.audioFilesForPlayer = new List<List<AudioFileForGame>> ();
		this.audioSettingsForMenu = new List<string> ();
		this.audioSettingsForPlayers = new List<string> ();
	} /* End private void initialiseLists () */



	/**
	 * Check if a player exists.
	 * 
	 * This private method checks if a player exists by
	 * checking if the specified player's index is in range.
	 * 
	 * @param nOfPlayer - the index of the player (int)
	 * @return bool - True if the player exists, false otherwise
	 * @access Private
	 * @author Konstantinos Drossos
	 */ 
	private bool existsPlayer (int nOfPlayer) {
		if (nOfPlayer > this.nOfPlayers_max || nOfPlayer < this.nOfPlayers_min) return false;
		else return true;
	} /* End private bool playerExists (int nOfPlayer) */


	/*!< Number of maximum players for the game */
	private int nOfPlayers_max;	
	/*!< Number of minimum players for the game */
	private int nOfPlayers_min;	
	/*!< The name of the current game */
	private string gameName; 

	/*!< The players' settings */
	private List<string> audioSettingsForPlayers; 
	/*!< The available audio menu settings */
	private List<string> audioSettingsForMenu; 
	/*!< The audio files for each player */
	private List<List<AudioFileForGame>> audioFilesForPlayer; 
	/*!< The audio files for the game menu */
	private List<AudioFileForGame> audioFilesForMenu;

	/*!< The current audio settings for menu */
	private string currentAudioSettingForMenu;
	/*!< The current audio settings for player(s) */
	private string currentAudioSettingForPlayer;

}

/* Scripts/SoundGraphicsEngine/AudioFilesSettings.cs */
/* END OF FILE */
