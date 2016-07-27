using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    public static bool just_started = true;
    public static bool first_game = true;
    public static bool auto_select = false;
    public static string game_sounds = "default";
    public static string menu_sounds = "default";
    public static string previousMenu = "mainMenu";
    public static string[] soundsets;
    public static readonly Settings instance = new Settings();
    public static string default_soundset = "Θοδωρής";
    public static AudioSettingsDocument audio_settings_document;
    public static Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
}