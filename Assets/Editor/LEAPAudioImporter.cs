/**
 * The file of LEAPAudioImporter class. 
 * 
 * This file holds the declaration and 
 * implementation of the LEAPAudioImporter class.
 * 
 * @file LEAPAudioImporter.cs
 * @version 1.0
 * @date 21/01/2015 (dd/mm/yyyy)
 * @author Konstantinos Drossos
 * @copyright ??? distributed as is under MIT Licence.
 */
using UnityEngine;
using UnityEditor;


/**
 * Class to handle the import of audio files.
 * 
 * This class pre-process the audio files and sets
 * the desired settings for them. 
 * 
 * @see AssetPostprocessor
 * @author Konstantinos Drossos
 */
public class LEAPAudioImporter : AssetPostprocessor {

	/**
	 * Audio pre-processing.
	 * 
	 * This private method is called on audio pre-processing
	 * when each audio file is imported on the editor. 
	 * 
	 * @author Konstantinos Drossos
	 */
	private void OnPreprocessAudio() {
		AudioImporter importer = (AudioImporter)assetImporter;

		importer.forceToMono = false;
		importer.threeD = false;

	}



	/**
	 * Audio post-processing.
	 * 
	 * This private method is called for the post-processing
	 * of audio assets. It accepts the AudioClip object to be
	 * processed and operates on it. Currently is just a stub
	 * method serving for future needs. 
	 * 
	 * @param audioClip - the AudioClip object to be processed (AudioClip)
	 * @author Konstantinos Drossos
	 */
	private void OnPostprocessAudio(AudioClip audioClip) {
	}
}
