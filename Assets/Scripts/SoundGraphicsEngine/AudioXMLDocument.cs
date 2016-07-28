
/**
 * Copyright 2016 , SciFY NPO - http://www.scify.org
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
/**
 * The file for the AudioXMLDocument class. 
 * 
 * This file holds the definition and implementation 
 * of AudioXMLDocument class. This file is part of
 * the LEAP project. 
 * 
 * @file AudioXMLDocument.cs
 * @version 1.0
 * @date 14/01/2015 (dd/mm/yyyy)
 * @author Konstantinos Drossos
 * @copyright SciFY distributed as is under Apache v2 License.
 */
using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;



/**
 * Class to handle the XML settings document.
 * 
 * This class inherits from the XmlDocument class
 * and is used to handle the XML document with the
 * audio settings for a game. 
 * 
 * @author Konstantinos Drossos
 */ 
public class AudioXMLDocument : XmlDocument {

	/**
	 * Default constructor.
	 * 
	 * The default constructor just instantiates
	 * a AudioXMLDocument object according to the 
	 * XmlDocument's class default constructor. 
	 * 
	 * @see XmlDocument
	 * @author Konstantinos Drossos
	 */
	public AudioXMLDocument (): base() {}



	/**
	 * Parses the audio settings XML document. 
	 * 
	 * This public method
	 * 
	 * @param gameName - game's name as appears in the settings' file name
	 * @throw ApplicationException - when the directory of settings not exists
	 * @throw FileNotFoundException - if the settings file not found
	 * @author Konstantinos Drossos
	 */
	public void LoadSettingsXML (string gameName) {

		string theSettingsFileName;
		
		if (Application.platform.Equals (RuntimePlatform.WindowsEditor) || 
						Application.platform.Equals (RuntimePlatform.OSXEditor)) {
			
			if (!Directory.Exists (AudioXMLDocument.settingsBaseDir)) 
				throw new ApplicationException ("Error to audio settings path!" + 
                        AudioXMLDocument.messageParentPathNotFound);

			DirectoryInfo theInfo = new DirectoryInfo (AudioXMLDocument.settingsBaseDir);
			FileInfo[] files = theInfo.GetFiles ("*_" + gameName + ".xml");
			if (files.Length < 1) throw new FileNotFoundException ("Audio settings file not found!");
			theSettingsFileName = AudioXMLDocument.settingsBaseDir + 
				files [0].Name.Substring (0, files [0].Name.Length - 4) + 
					".xml";
			this.Load (theSettingsFileName);
		} else {
			theSettingsFileName = "sound_settings_" + gameName;
			TextAsset xmlText = (TextAsset)Resources.Load (theSettingsFileName);
			this.LoadXml (xmlText.text);
		}
	}

	/**
	 * Property for the maximum amount of players. 
	 * 
	 * This public property is get only and returns
	 * the maximum amount of players for the specified
	 * game, as it is defined in the settings XML document. 
	 * 
	 * @return int - the maximum amount of players
	 * @author Konstantinos Drossos
	 */
	public int MaximumPlayers {
		get {
			return Convert.ToInt16(this.DocumentElement.SelectSingleNode (
				AudioXMLDocument.xmlSettingsNameOfNodeBase + 
				AudioXMLDocument.xmlSettingsNameOfNodeGeneralSettings + 
				AudioXMLDocument.xmlSettingsNameOfNodePlayers + 
				AudioXMLDocument.xmlSettingsNameOfNodeMaxPlayers).InnerText);
		}
	}



	/**
	 * Property for the minimum amount of players. 
	 * 
	 * This public property is get only and returns
	 * the minimum amount of players for the specified
	 * game, as it is defined in the settings XML document. 
	 * 
	 * @return int - the minimum amount of players
	 * @author Konstantinos Drossos
	 */
	public int MinimumPlayers {
		get {
			return Convert.ToInt16(this.DocumentElement.SelectSingleNode (
				AudioXMLDocument.xmlSettingsNameOfNodeBase + 
				AudioXMLDocument.xmlSettingsNameOfNodeGeneralSettings + 
				AudioXMLDocument.xmlSettingsNameOfNodePlayers + 
				AudioXMLDocument.xmlSettingsNameOfNodeMinPlayers).InnerText);
		}
	}



	/**
	 * Returns the available settings for a game's element.
	 * 
	 * This public method returns the available settings of
	 * the specified game and for the element of the game that
	 * is passed as argument to the method. E.g., it can return
	 * the available seetings for players or menu. The element
	 * of the game must be specified in the XML document with 
	 * the settings. 
	 * 
	 * @param ofWhom - the element of the game (string).
	 * @return List<string> - the available settings for the specified element
	 * @author Konstantinos Drossos
	 */
	public List<string> getAudioSettingsOf(string ofWhom) {
		XmlNode settingsNode = this.SelectSingleNode (AudioXMLDocument.xmlSettingsNameOfNodeBase + 
		                                              AudioXMLDocument.xmlSettingsNameOfNodeGeneralSettings + 
		                                              AudioXMLDocument.xmlSettingsNameOfNodeSettings + 
		                                              "/setting");
		
		while (!settingsNode.Attributes["case"].InnerText.Equals(ofWhom)) {
			settingsNode = settingsNode.NextSibling;
			if (settingsNode == null) throw new KeyNotFoundException ("no audio settings for " + ofWhom);
		}
		
		List<string> toReturn = new List<string> ();
		foreach (XmlNode tmpNode in settingsNode.ChildNodes) toReturn.Add(tmpNode.InnerText);
		return toReturn;
	}



	/**
	 * Gets the paths of the player's audio files.
	 * 
	 * This public method returns the paths for the sound of a player and
	 * specified settings. Both player and settings are given as arguments to
	 * the method.
	 * 
	 * @param playerIndex - the index of the player (int)
	 * @param forSettings - the settings for which the file is sought
	 * @return List<string> - the list of the audio files
	 * @author Konstantinos Drossos
	 */
	public List<AudioFileForGame> getFilesForPlayer(int playerIndex, string forSettings) {

		List<AudioFileForGame> toReturn;
		
		XmlNode audioNode = this.DocumentElement.SelectSingleNode(
			AudioXMLDocument.xmlSettingsNameOfNodeBase + 
			AudioXMLDocument.xmlSettingsNameOfNodeAudio);

		audioNode = audioNode.SelectSingleNode ("player");

		while (Convert.ToInt16(audioNode.Attributes["index"].InnerText) != playerIndex + 1) {
			audioNode = audioNode.NextSibling;
			if (audioNode == null) throw new Exception () ;
		}

		audioNode = audioNode.SelectSingleNode ("settings");

		while (audioNode.Attributes["name"].InnerText != forSettings) {
			audioNode = audioNode.NextSibling;
			if (audioNode == null) throw new Exception ("No audio nodes found for: " + forSettings);
		}

		string tmpString;
		string tmpPath;
		string[] posStrings;
		
		toReturn = new List<AudioFileForGame> ();

		foreach(XmlNode tmpAudioFile in audioNode.ChildNodes) {
				
			tmpString = tmpAudioFile.SelectSingleNode("position_vals").InnerText;
			posStrings = tmpString.Split('=');
			tmpPath = tmpAudioFile.SelectSingleNode("path").InnerText;
			
			if (tmpPath.Substring(0, 1).Equals("\"")) tmpPath = tmpPath.Substring(1);
			
			if (tmpPath.Substring(tmpPath.Length-1, 1).Equals("\"")) 
				tmpPath = tmpPath.Substring(0, tmpPath.Length-1);
			if (tmpPath.Substring(tmpPath.Length - 4).Equals(".")) 
				tmpPath = tmpPath.Substring(0, tmpPath.Length-4);
			
			toReturn.Add( new AudioFileForGame(tmpAudioFile.Attributes["case"].InnerText,
			                     new UnityEngine.Vector3(Convert.ToSingle(posStrings[0]), 
			                        Convert.ToSingle(posStrings[1]), 
			                        Convert.ToSingle(posStrings[2])),
			                     "Sounds/" + tmpPath));
		}

		return toReturn;
	}



	/**
	 * Gets the paths of the menu's audio files.
	 * 
	 * This public method returns the paths for the sound of the menu and
	 * specified settings. The settings are given as argument to
	 * the method.
	 * 
	 * @param forSettings - the settings for which the file is sought
	 * @return List<string> - the list of the audio files
	 * @author Konstantinos Drossos
	 */
	public List<AudioFileForGame> getFilesForMenu(string forSettings) {

		List<AudioFileForGame> toReturn;

		XmlNode audioNode = this.DocumentElement.SelectSingleNode(
			AudioXMLDocument.xmlSettingsNameOfNodeBase + 
			AudioXMLDocument.xmlSettingsNameOfNodeAudio);

		audioNode = audioNode.SelectSingleNode ("menu");

		audioNode = audioNode.SelectSingleNode ("settings");
		
		while (audioNode.Attributes["name"].InnerText != forSettings) {
			audioNode = audioNode.NextSibling;
			if (audioNode == null) throw new Exception ("No settings found for: " + forSettings) ;
		}

		string tmpPath;
		toReturn = new List<AudioFileForGame> ();

		foreach (XmlNode tmpAudioFile in audioNode.ChildNodes) {
			tmpPath = tmpAudioFile.SelectSingleNode("path").InnerText;
			
			if (tmpPath.Substring(0, 1).Equals("\"")) tmpPath = tmpPath.Substring(1);
			
			if (tmpPath.Substring(tmpPath.Length-1, 1).Equals("\"")) 
				tmpPath = tmpPath.Substring(0, tmpPath.Length-1);
			if (tmpPath.Substring(tmpPath.Length - 4).Equals(".")) 
				tmpPath = tmpPath.Substring(0, tmpPath.Length-4);
			
			toReturn.Add( new AudioFileForGame(tmpAudioFile.Attributes["case"].InnerText, "Sounds/" + tmpPath));
		}

		return toReturn;
	}


	/*!< Default base dir for the settings file */
	private static string settingsBaseDir = Application.dataPath + "/Resources/"; 
	/*!< Message for path not exists */
	private static string messageParentPathNotFound = "Path does not exist"; 
	/*!< The base node of the settings' xml file */
	private static string xmlSettingsNameOfNodeBase = "/theFile"; 
	/*!< The general settings node of xml file */
	private static string xmlSettingsNameOfNodeGeneralSettings = "/general"; 
	/*!< The player's node of xml file */
	private static string xmlSettingsNameOfNodePlayers = "/players";
	/*!< The max players' node of xml file */
	private static string xmlSettingsNameOfNodeMaxPlayers = "/maximum";
	/*!< The min players' node of xml file */
	private static string xmlSettingsNameOfNodeMinPlayers = "/minimum";
	/*!< The audio node of xml file */
	private static string xmlSettingsNameOfNodeAudio = "/audio";
	/*!< The settings node of xml file */
	private static string xmlSettingsNameOfNodeSettings = "/settings";
}

/* Scripts/SoundGraphicsEngine/AudioXMLDocument.cs */
/* END OF FILE */
