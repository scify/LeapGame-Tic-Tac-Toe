
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
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class AudioSettingsDocument : AudioXMLDocument {

    protected readonly List<string> audioSettings = new List<string>();
    protected readonly Dictionary<string, List<AudioFileForGame>> filesForMenu = new Dictionary<string, List<AudioFileForGame>>();
    protected readonly Dictionary<string, Dictionary<int, List<AudioFileForGame>>> filesForPlayer = new Dictionary<string, Dictionary<int, List<AudioFileForGame>>>();

    public void LoadSettingsXML(string gameName) {
        base.LoadSettingsXML(gameName);
        filesForMenu.Add("default", base.getFilesForMenu("default"));
        filesForPlayer.Add("default", new Dictionary<int, List<AudioFileForGame>>());
        for (int i = 0; i < base.MaximumPlayers; i++) {
            filesForPlayer["default"].Add(i, base.getFilesForPlayer(i, "default"));
        }
    }

    public List<string> getAudioSettingsOf(string ofWhom) {
        List<string> res = base.getAudioSettingsOf(ofWhom);
        res.AddRange(audioSettings);
        return res;
    }

    public List<AudioFileForGame> getFilesForPlayer(int playerIndex, string forSettings) {
        return filesForPlayer[forSettings][playerIndex];
    }

    public List<AudioFileForGame> getFilesForMenu(string forSettings) {
        return filesForMenu[forSettings];
    }

    public void addAudioSetting(string theSetting) {
        if (!audioSettings.Contains(theSetting)) {
            audioSettings.Add(theSetting);
            filesForMenu.Add(theSetting, new List<AudioFileForGame>());
            foreach (AudioFileForGame af in filesForMenu["default"]) {
                filesForMenu[theSetting].Add(new AudioFileForGame(af.TheCase, af.ThePosition, af.ThePath));
            }
            filesForPlayer.Add(theSetting, new Dictionary<int, List<AudioFileForGame>>());
            for (int i = 0; i < base.MaximumPlayers; i++) {
                filesForPlayer[theSetting].Add(i, new List<AudioFileForGame>());
                foreach (AudioFileForGame af in filesForPlayer["default"][i]) {
                    filesForPlayer[theSetting][i].Add(new AudioFileForGame(af.TheCase, af.ThePosition, af.ThePath));
                }
            }
        }
    }

    public void addFiles(string name, FileInfo[] files) {
        if (files.Length == 0) {
            return;
        }
        Dictionary<string, AudioFileForGame> audioFiles = new Dictionary<string, AudioFileForGame>();
        foreach (AudioFileForGame af in filesForMenu[name]) {
            audioFiles.Add(af.ThePath.Substring(af.ThePath.LastIndexOf("/") + 1), af);
        }
        foreach (FileInfo file in files) {
            if (audioFiles.ContainsKey(file.Name)) {
                audioFiles[file.Name].ThePath = file.FullName;
                Settings.audioClips.Add(file.FullName, null);
            }
        }
    }

    public void addPlayerFiles(string name, int index, FileInfo[] files) {
        if (files.Length == 0) {
            return;
        }
        Dictionary<string, AudioFileForGame> audioFiles = new Dictionary<string, AudioFileForGame>();
        foreach (AudioFileForGame af in filesForPlayer[name][index]) {
            audioFiles.Add(af.ThePath.Substring(af.ThePath.LastIndexOf("/") + 1), af);
        }
        foreach (FileInfo file in files) {
            if (audioFiles.ContainsKey(file.Name)) {
                audioFiles[file.Name].ThePath = file.FullName;
                Settings.audioClips.Add(file.FullName, null);
            }
        }
    }

    public void addPlayerFile(string name, string prefix, FileInfo file) {
        filesForPlayer[name][0].Add(new AudioFileForGame(prefix + "_" + file.Name.Replace(".wav", ""), Vector3.zero, file.FullName));
        Settings.audioClips.Add(file.FullName, null);
    }
}
