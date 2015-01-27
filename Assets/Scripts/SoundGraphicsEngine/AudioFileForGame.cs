/**
 * The file of AudioFileForGame class. 
 * 
 * This file holds the declaration and 
 * implementation of the AudioFileForGame class.
 * 
 * @file AudioFileForGame.cs
 * @version 1.0
 * @date 21/01/2015 (dd/mm/yyyy)
 * @author Konstantinos Drossos
 * @copyright ??? distributed as is under MIT Licence.
 */
using System;
using UnityEngine;



/**
 * Class to represent an audio file for a game.
 * 
 * This class holds all the useful information about
 * an audio file for a game. 
 * 
 * @author Konstantinos Drossos
 */
public class AudioFileForGame {



	/**
	 * Simple constructor.
	 * 
	 * This constructor accepts the case and the path 
	 * of a sound. 
	 * 
	 * @param theCase - the case of the sound (string)
	 * @param thePath - the path of the audio file
	 * @author Konstantinos Drossos
	 */
	public AudioFileForGame (string theCase, string thePath) : 
	this(theCase, new UnityEngine.Vector3(0, 0, 0), thePath) {}



	/**
	 * Full constructor.
	 * 
	 * This constructor accepts the case, the path and
	 * the position of a sound. 
	 * 
	 * @param theCase - the case of the sound (string)
	 * @thePosition - the position of the sound (UnityEngine.Vector3)
	 * @param thePath - the path of the audio file
	 * @author Konstantinos Drossos
	 */
	public AudioFileForGame(string theCase, UnityEngine.Vector3 thePosition, string thePath) {
		this.theCase = theCase;
		this.thePosition = thePosition;
		this.thePath = thePath;
	}



	/**
	 * Property for the case.
	 * 
	 * This property is get and set able. It
	 * reflects the case of the sound.
	 * 
	 * @param value - the case of the sound (string)
	 * @author Konstantinos Drossos
	 */
	public string TheCase {
		get { return theCase; }
		set { theCase = value; }
	}



	/**
	 * Property for the position.
	 * 
	 * This property is get and set able. It
	 * reflects the position of the sound.
	 * 
	 * @param value - the position of the sound (UnityEngine.Vector3)
	 * @author Konstantinos Drossos
	 */
	public UnityEngine.Vector3 ThePosition {
		get { return thePosition; }
		set { thePosition = value; }
	}



	/**
	 * Property for the path.
	 * 
	 * This property is get and set able. It
	 * reflects the path of the sound.
	 * 
	 * @param value - the path of the sound (string)
	 * @author Konstantinos Drossos
	 */
	public string ThePath {
		get { return thePath; }
		set { thePath = value; }
	}
	
	/*!< The case of sound reproduction */
	private string theCase; 
	/*!< The position of sound reproduction */
	private UnityEngine.Vector3 thePosition; 
	/*!< The path of sound file */
	private string thePath; 
}

/* Scripts/SoundGraphicsEngine/AudioFileForGame.cs */
/* END OF FILE */
