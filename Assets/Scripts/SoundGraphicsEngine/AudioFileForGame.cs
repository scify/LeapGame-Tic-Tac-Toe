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

public class AudioFileForGame {

	public AudioFileForGame (string theCase, string thePath) : 
	this(theCase, new UnityEngine.Vector3(0, 0, 0), thePath) {}
	
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
	
	/*!< The case of sound reproduction */
	private string theCase; 
	/*!< The position of sound reproduction */
	private UnityEngine.Vector3 thePosition; 
	/*!< The path of sound file */
	private string thePath; 
}

/* Scripts/SoundGraphicsEngine/AudioFileForGame.cs */
/* END OF FILE */
