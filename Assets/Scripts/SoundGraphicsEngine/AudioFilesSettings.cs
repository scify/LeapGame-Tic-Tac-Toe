using System;
using System.Collections.Generic;
using System.IO;
//using System.IO.IOException;
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
	 * @access Public
	 * @author Konstantinos Drossos
	 * @param gameName - the name of the game to be played (string)
	 */
	public AudioFilesSettings (string gameName) {

		if (!Directory.Exists (AudioFilesSettings.dirSettings))
						throw new ApplicationException ("Error to audio settings path!");

		/* First get all files from the sound settings XML */
		try {
			DirectoryInfo theInfo = new DirectoryInfo (AudioFilesSettings.dirSettings);

			FileInfo[] files = theInfo.GetFiles("*_" + gameName + ".xml");

			if (files.Length < 1) throw new FileNotFoundException ("Audio settings file not found!");

			XmlDocument gameSettings = new XmlDocument();
			gameSettings.Load(AudioFilesSettings.dirSettings + files[0].Name);

			/* Get the amount of maximum players */
			XmlNode tmpNode = gameSettings.DocumentElement.SelectSingleNode("//general/players/maximum");
			this.nOfPlayers_max = Convert.ToInt16(tmpNode.InnerText);

			/* Get the amount of minimum players */
			tmpNode = gameSettings.DocumentElement.SelectSingleNode("//general/players/minimum");
			this.nOfPlayers_min = Convert.ToInt16(tmpNode.InnerText);

			/* Get all settings */
			tmpNode = gameSettings.DocumentElement.SelectSingleNode("//audio");
			int playerTmpIndx;
			string tmpString;

			foreach (XmlNode playerNode in tmpNode.ChildNodes) {

				playerTmpIndx = Convert.ToInt16(playerNode.Attributes["index"].InnerText) - 1;

				foreach(XmlNode tmpSetting in playerNode.ChildNodes) {

					this.settingsNames.Add(tmpSetting.Attributes["name"].InnerText);

					foreach(XmlNode tmpAudioFile in tmpSetting.ChildNodes) {

						this.casesOfAudioFiles.Add(tmpAudioFile.Attributes["case"].InnerText);
						this.pathOfAudioFiles[playerTmpIndx].Add(tmpAudioFile.SelectSingleNode("/path").InnerText);
						tmpString = tmpAudioFile.SelectSingleNode("/position_vals").InnerText;
						this.positionsForAudioFiles.Add(new UnityEngine.Vector3(Convert.ToSingle(tmpString[0]), Convert.ToSingle(tmpString[1]), Convert.ToSingle(tmpString[2])));

					}
				}
			}

		} catch (Exception) {
			// TODO Add exception handling just in case!
		}

	}

	public string getPlayerSoundsPath (int nOfPlayer) {

		if (this.playerExists(nOfPlayer))
			throw new ArgumentOutOfRangeException ("nOfPlayer", nOfPlayer, this.messageGreaterPlayerIndex);

		return "OK";
	}

	/**
	 * Gets the specific audio files paths for all players.
	 * 
	 * This public method returns the actual paths in which
	 * are the audio files for all players. The paths are 
	 * returned in a List<string> object where the index of the
	 * strings is the corresponding index of the player. 
	 * 
	 * @access Public
	 * @author Konstantinos Drossos
	 * @return List<string> - the actual audio files' paths for all players
	 */ 
	public List<string> getPlayersPaths() {
		return null;
	}

	public string getPlayerSettingsName (int nOfPlayer) {
		return null;
	}

	public void setPlayerPath(int nOfPlayer, string newPath) {
	}

	public string getPathForSound(int nOfPlayer, string theCase, Vector3 position) {

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


	/**
	 * Check if a player exists.
	 * 
	 * This private method checks if a player exists by
	 * checking if the specified player's index is in range.
	 * The range is calculated by the total amount of allowed
	 * players for the specified game. 
	 * 
	 * @access Private
	 * @author Konstantinos Drossos
	 * @param nOfPlayer - the index of the player (int)
	 * @return bool - True if the player exists, false otherwise
	 */ 
	private bool playerExists (int nOfPlayer) {
		if (nOfPlayer > this.nOfPlayers_max || nOfPlayer < this.nOfPlayers_min) return false;
		else return true;
	}

	private Dictionary<string, List<string> > getSettingsFromFile() {
		return this.getSettingsFromFile (this.defaultSettingsPath, this.settingsFileName);
	}

	private Dictionary<string, List<string> > getSettingsFromFile(string settingsFileName) {
		return this.getSettingsFromFile (this.defaultSettingsPath, settingsFileName);
	}

	private Dictionary<string, List<string> > getSettingsFromFile(string thePath, string settingsFileName) {
		return null;
		
	}

	/*!< Message for exception of player index */
	private string messageGreaterPlayerIndex = "The specified index of player is greater than the amount of total players";
	
	/*!< Message for path not exists */
	private string messageParentPathNotFound = "Path does not exist";
	
	/*!< Path to each file for each player */
	private List<List<string> > pathOfAudioFiles;

	/*!< List of cases for each audio file */
	private List<string> casesOfAudioFiles;

	/*!< List of positions' values for each audio file */
	private List<UnityEngine.Vector3> positionsForAudioFiles;

	private List<string> settingsNames;
	
	/*!< Number of players */
	private int nOfPlayers_max;	
	private int nOfPlayers_min;	

	/*!< The path for the file holding the paths for the default audio files */
	private string defaultSettingsPath = "Assets/Resources/Sounds/TicTacToe/default/";

	/*!< The name of the file holding the audio files' settings */
	private string settingsFileName = "audioSettings.xml";

	/*!< The name of the directory which will hold the audio settings files */
	private static readonly string dirSettings = "Assets/Resources/Sounds";
}

