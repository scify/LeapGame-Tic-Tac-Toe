using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IOException;
using System.Xml;

/**
 * The audio file settings for players.
 * 
 * This class handles the audio file settings for
 * all players in a specified game. 
 * 
 * @author Konstantinos Drossos
 */
public class AudioFilesSettings {

	/** 
	 * Constructor of the AudioFilesSettings class.
	 * 
	 * The constructor of the AudioFilesSettings class accepts
	 * the total amount of players for the intented game and the
	 * base/parent path of the audio files. 
	 * 
	 * @access Public
	 * @author Konstantinos Drossos
	 * @param nOfPlayers - the amount of maximum players for the game (int)
	 * @parm parentPath - the parent path of the audio files (string)
	 */
	public AudioFilesSettings (int nOfPlayers, string parentPath) {

		/* First check if the specified parent path exists. If
		   not, issue a proper exception. */
		if (!File.Exists(parentPath)) 
			throw new FileNotFoundException(this.messageParentPathNotFound, parentPath);

		/* If all good, assign the number of players for this game */
		this.nOfPlayers = nOfPlayers;
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
	}

	public string getPlayerSettingsName (int nOfPlayer) {
	}

	public void setPlayerPath(int nOfPlayer, string newPath) {
	}

	public string getPathForSound(int nOfPlayer, string theCase) {
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
		return nOfPlayer > this.nOfPlayers ? false : true;
	}

	private Dictionary<string, List<string> > getSettingsFromFile() {
		return this.getSettingsFromFile (this.defaultSettingsPath, this.settingsFileName);
	}

	private Dictionary<string, List<string> > getSettingsFromFile(string settingsFileName) {
		return this.getSettingsFromFile (this.defaultSettingsPath, settingsFileName);
	}

	private Dictionary<string, List<string> > getSettingsFromFile(string thePath, string settingsFileName) {
		
	}

	/*!< Message for exception of player index */
	private string messageGreaterPlayerIndex = "The specified index of player is greater than the amount of total players";
	
	/*!< Message for path not exists */
	private string messageParentPathNotFound = "Path does not exist";
	
	/*!< Path to files for each player */
	private Dictionary<int, string> audioFilesForPlayers;	
	
	/*!< Number of players */
	private int nOfPlayers;	

	/*!< The path for the file holding the paths for the default audio files */
	private string defaultSettingsPath = "Assets/Resources/Sounds/TicTacToe/default/";

	/*!< The name of the file holding the audio files' settings */
	private string settingsFileName = "audioSettings.xml";
}

