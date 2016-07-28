
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
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class TTTLoadingInitiator : MonoBehaviour {
    
    private TextCanvasObject gui;
    private string basepath;
    private int players;
    private int total = 0;
    private int done = 0;
    private int bypass = 0;

    void Start() {
        TTTStateRenderer renderer = new TTTStateRenderer();

        List<WorldObject> environment = new List<WorldObject>();
        environment.Add(new TTTStaticObject("Prefabs/TTT/Camera_Default", new Vector3(0, 10, 0), false));
        environment.Add(new TTTStaticObject("Prefabs/TTT/Light_Default", new Vector3(0, 10, 0), false));
        environment.Add(new CanvasObject("Prefabs/TTT/OutroLogo", true, new Vector3(0, 0, 0), false));
        gui = new TextCanvasObject("Prefabs/TTT/GUI_Element", true, "Reading soundsets", new Vector3(0, 0, 0), false);
        environment.Add(gui);

        TTTRuleset rules = new TTTRuleset();
        rules.Add(new TTTRule("initialization", (TTTMenuState state, GameEvent eve, TTTMenuEngine engine) => {
            readSoundsets();
            return false;
        }));

        gameObject.AddComponent<TTTMenuEngine>();
        gameObject.AddComponent<TTTMenuUserInterface>();
        gameObject.GetComponent<TTTMenuEngine>().initialize(rules, environment, renderer);
        gameObject.GetComponent<TTTMenuUserInterface>().initialize(gameObject.GetComponent<TTTMenuEngine>());
        gameObject.GetComponent<TTTMenuEngine>().postEvent(new GameEvent("", "initialization", "unity"));
    }

    void Update() {
        if (bypass == 1 || (total != 0 && done == total)) {
            Application.LoadLevel(1);
        }
    }

    void readSoundsets() {
        try {
            Settings.audio_settings_document = new AudioSettingsDocument();
            Settings.audio_settings_document.LoadSettingsXML("Tic-Tac-Toe");
        } catch (Exception e) {
            Debug.Log(e);
            Application.Quit();
        }
        players = Settings.audio_settings_document.MaximumPlayers;
        basepath = Application.dataPath + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar;
        basepath = basepath.Replace("_Data", "_Soundsets");
        DirectoryInfo dir = new DirectoryInfo(basepath);
        if (dir.Exists) {
            foreach (DirectoryInfo d in dir.GetDirectories()) {
                string name = null;
                if (d.GetFiles("info").Length != 0) {
                    FileInfo infoFile = d.GetFiles("info")[0];
                    string[] data = File.ReadAllLines(infoFile.FullName);
                    foreach (string s in data) {
                        if (s.Contains("name=")) {
                            name = s.Replace("name=", "");
                        }
                    }
                    Settings.audio_settings_document.addAudioSetting(name);
                    gui.text = "Loading " + name + "\n";
                    loadSoundset(basepath + d.Name + Path.DirectorySeparatorChar, name);
                }
            }
            if (total == 0) {
                bypass = 1;
            }
        } else {
            bypass = 1;
        }
    }

    void loadSoundset(string path, string name) {
        DirectoryInfo menu = new DirectoryInfo(path + "menu_sounds");
        Settings.audio_settings_document.addFiles(name, menu.GetFiles());
        for (int i = 1; i <= Settings.audio_settings_document.MaximumPlayers; i++) {
            DirectoryInfo player = new DirectoryInfo(path + "player" + i);
            Settings.audio_settings_document.addPlayerFiles(name, i, player.GetFiles());
        }
        foreach (FileInfo file in new DirectoryInfo(path).GetFiles("*.wav")) {
            Settings.audio_settings_document.addPlayerFile("default", name, file);
        }
        foreach (string key in Settings.audioClips.Keys) {
            if (Settings.audioClips[key] == null) {
                total++;
                StartCoroutine(loadClip(key));
            }
        }
    }

    IEnumerator<System.Object> loadClip(string path) {
        FileInfo f = new FileInfo(path);
        List<string> tokens = new List<string>();
        tokens.Add(f.Name);
        DirectoryInfo dir = f.Directory;
        while (dir != null) {
            tokens.Insert(0, dir.Name);
            dir = dir.Parent;
        }
        tokens[0] = tokens[0].Replace("\\", "").Replace("/", "");
        string url = "file:/";
        if (Application.platform.ToString().Contains("Windows")) {
            url += "/";
        }
        foreach (string s in tokens) {
            url += "/" + s;
        }
        WWW www = new WWW(url);
        yield return www;
        Settings.audioClips[path] = www.audioClip;
        done++;
    }
}