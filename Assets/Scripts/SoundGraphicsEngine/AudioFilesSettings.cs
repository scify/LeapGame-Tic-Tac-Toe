/**
 * @file AudioFilesSettings.cs
 * @author Konstantinos Drossos
 */ 

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

/**
 * The audio file settings for players.
 * 
 * This class handles the audio file settings for
 * all players in a specified game. 
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
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public AudioFilesSettings (string gameName) {

		/* Check just in case */
        if (!Directory.Exists(AudioFilesSettings.settingsBaseDir))
			throw new ApplicationException ("Error to audio settings path!" + AudioFilesSettings.messageParentPathNotFound);
		
		/* First get all files from the sound settings XML */
		try {

			DirectoryInfo theInfo = new DirectoryInfo(AudioFilesSettings.settingsBaseDir);

			FileInfo[] files = theInfo.GetFiles("*_" + gameName + ".xml");

			if (files.Length < 1) throw new FileNotFoundException ("Audio settings file not found!");

			/* Initiliase the Lists that are used */
			this.theAudioFiles = new List<List<AudioFileForGame>> ();

			/* Get the Xml Document */
			XmlDocument gameSettings = new XmlDocument();
			gameSettings.Load(AudioFilesSettings.settingsBaseDir + files[0].Name);

			/* Get the amount of maximum players */
			XmlNode tmpNode = gameSettings.DocumentElement.SelectSingleNode("/theFile/general/players/maximum");
			this.nOfPlayers_max = Convert.ToInt16(tmpNode.InnerText);

			/* Get the amount of minimum players */
			tmpNode = gameSettings.DocumentElement.SelectSingleNode("/theFile/general/players/minimum");
			this.nOfPlayers_min = Convert.ToInt16(tmpNode.InnerText);

			/* Get all settings */
            tmpNode = gameSettings.DocumentElement.SelectSingleNode("/theFile/audio");
			int playerTmpIndx;
			string tmpString;
            string[] posStrings;

			foreach (XmlNode playerNode in tmpNode.ChildNodes) {

				/* The player index */
				playerTmpIndx = Convert.ToInt16(playerNode.Attributes["index"].InnerText) - 1;
				this.theAudioFiles.Add(new List<AudioFileForGame>());

				/* Iterate through XML nodes and get the info needed */
				foreach(XmlNode tmpSetting in playerNode.ChildNodes) {

					//this.audioFilesSettingsNames.Add(tmpSetting.Attributes["name"].InnerText);

					foreach(XmlNode tmpAudioFile in tmpSetting.ChildNodes) {
                        tmpString = tmpAudioFile.SelectSingleNode("position_vals").InnerText;
                        posStrings = tmpString.Split('=');

						this.theAudioFiles[playerTmpIndx].Add(new AudioFileForGame(tmpAudioFile.Attributes["case"].InnerText,
                            new UnityEngine.Vector3(Convert.ToSingle(posStrings[0]), Convert.ToSingle(posStrings[1]), Convert.ToSingle(posStrings[2])),
                            "Sounds/" + tmpAudioFile.SelectSingleNode("path").InnerText + ".wav"));
					}
				}
			}

		} catch (Exception e) {
            Debug.Log(e.Message);
			// TODO Add exception handling just in case!
		}

	} /* End public AudioFilesSettings */



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

		return this.theAudioFiles [player].Find (
			delegate (AudioFileForGame af) {
						if (af.TheCase.Equals (theCase) && af.ThePosition.Equals (soundOrigin))
								return true;
						else
								return false;
				}).ThePath;
	}



	/**
	 * Returns the verbal descriptions of positions for sounds.
	 * 
	 * This public method returns all acceptable verbal descriptions
	 * of positions for the sounds.
	 * 
	 * @return List<string> - the verbal descriptions of positions
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public List<string> getAllSoundsCases() {
		return this.audioFilesCases;
	} /* End public List<string> getAllSoundsCases() */



	/**
	 * Returns the position of sound for a given case and player.
	 * 
	 * This public method accepts a verbal description of position
	 * and returns the actual vector with position for that case.
	 *
	 * @param nOfPlayer - the index of player (int)
	 * @param theCase - the verbal description of position (string)
	 * @return UnityEngine.Vector3 - the position
	 * @exception ArgumentOutOfRangeException - if the index of player is out of range
	 * @exception KeyNotFoundException - if the verbal description does not exists
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public UnityEngine.Vector3 getSoundPosition(int nOfPlayer, string theCase) {

		/* Check for bad arguments */
		if (!this.existsPlayer (nOfPlayer) ) throw new ArgumentOutOfRangeException("nOfPlayer", nOfPlayer, AudioFilesSettings.messageGreaterPlayerIndex);
		if (!this.existsCase (theCase)) throw new KeyNotFoundException ("Case: " + theCase + " not found!");

		/* Find the case based on player index */


		return new UnityEngine.Vector3();
	} /* End public UnityEngine.Vector3 getSoundPosition(int nOfPlayer, string theCase) */



	/**
	 * Returns all paths of sounds for a player.
	 * 
	 * This public method accepts a player index and 
	 * returns the paths of all sounds for that player.
	 * 
	 * @param nOfPlayer - the index of player (int)
	 * @return List<string> - the paths of all sounds for the player
	 * @exception ArgumentOutOfRangeException - if the index of player is out of range
	 * @access Public
	 * @author Konstantinos Drossos
	 */
	public List<string> getPathForAllSoundsOfPlayer (int nOfPlayer) {

		if (this.existsPlayer(nOfPlayer))
			throw new ArgumentOutOfRangeException ("nOfPlayer", nOfPlayer, AudioFilesSettings.messageGreaterPlayerIndex);

		return this.audioFilesPath [nOfPlayer];
	} /* End public List<string> getPathForAllSoundsOfPlayer (int nOfPlayer) */



	/**
	 * Gets the specific audio files paths for all players.
	 * 
	 * This public method returns the actual paths in which
	 * are the audio files for all players. The paths are 
	 * returned in a List<string> object where the index of the
	 * strings is the corresponding index of the player. 
	 * 
	 * @return List<string> - the actual audio files' paths for all players
	 * @access Public
	 * @author Konstantinos Drossos
	 */ 
	public List<string> getPlayersPaths() {
		return null;
	} /* End public List<string> getPlayersPaths() */



	public string getPlayerSettingsName (int nOfPlayer) {
		return null;
	}



	public void setPlayerPath(int nOfPlayer, string newPath) {
	}

	public string getPathForSound(int nOfPlayer, string theCase, Vector3 position) {

		return null;
	}


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



	/**
	 * Check if a case exists.
	 * 
	 * This private method checks if a case exists. If it does
	 * not returns false. Otherwise returns true. 
	 * 
	 * @param theCase - the case to check (string)
	 * @return bool - true if exists, false otherwise
	 * @access Private
	 * @author Konstantinos Drossos
	 */
	private bool existsCase (string theCase) {
		return this.audioFilesCases.Exists (c => c.Equals(theCase));
	} /* End private bool existsCase (string theCase) */



	private Dictionary<string, List<string> > getSettingsFromFile() {
		return this.getSettingsFromFile (AudioFilesSettings.settingsPathDefault, AudioFilesSettings.settingsFileName);
	}

	private Dictionary<string, List<string> > getSettingsFromFile(string settingsFileName) {
		return this.getSettingsFromFile (AudioFilesSettings.settingsPathDefault, settingsFileName);
	}

	private Dictionary<string, List<string> > getSettingsFromFile(string thePath, string settingsFileName) {
		return null;
		
	}

	/* ==============  Arguments section ==============  */

	private class AudioFileForGame {

		public AudioFileForGame(string theCase, UnityEngine.Vector3 thePosition, string thePath) {
			this.theCase = theCase;
			this.thePosition = thePosition;
			this.thePath = thePath;
		}

		public string TheCase {
			get { return theCase; }
			set { theCase = value; }
		}

		public UnityEngine.Vector3 ThePosition {
			get { return thePosition; }
			set { thePosition = value; }
		}

		public string ThePath {
			get { return thePath; }
			set { thePath = value; }
		}

		private string theCase;
		private UnityEngine.Vector3 thePosition;
		private string thePath;
	}
	
	private int nOfPlayers_max;	/*!< Number of maximum players for the game */
	private int nOfPlayers_min;	/*!< Number of minimum players for the game */

	private List<List<AudioFileForGame> > theAudioFiles;

	private List<List<string> > audioFilesPath; /*!< Path to each file for each player */
	private List<string> audioFilesCases; /*!< List of cases for each audio file */
	private List<UnityEngine.Vector3> audioFilesPositions; /*!< List of positions' values for each audio file */
	private List<string> audioFilesSettingsNames; /*!< List of settings for audio files (default, etc) */

    private static string settingsPathDefault = Application.dataPath + "/Resources/Sounds/TicTacToe/default/"; /*!< Default path for setting */
	private static string settingsFileName = "audioSettings.xml"; /*!< The name of the file holding the audio files' settings */
    private static string settingsBaseDir = Application.dataPath + "/Resources/Sounds/"; /*!< Default base dir for the settings file */
	private static string messageGreaterPlayerIndex = "The specified index of player is greater than the amount of total players"; /*!< Message for exception of player index */
	private static string messageParentPathNotFound = "Path does not exist"; /*!< Message for path not exists */
}

/* Scripts/SoundGraphicsEngine/AudioFilesSettings.cs */
/* END OF FILE */