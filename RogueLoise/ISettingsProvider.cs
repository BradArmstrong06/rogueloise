using System;
using System.Collections.Generic;
using System.IO;

namespace RogueLoise
{
    public interface ISettingsProvider<T>
    {
        T Setting { get; }

        string SettingsPath { get; set; }

        void LoadSettings();

        void SaveSettings();
    }

    public class SettingsProvider : ISettingsProvider<Settings>
    {
        public SettingsProvider(string path)
        {
            LoadSettings(path);
        }

        public Settings Setting { get; private set; }
        public string SettingsPath { get; set; }

        public void LoadSettings()
        {
            var settings = new Settings();

            if (!File.Exists(SettingsPath))
                return; //todo default settings

            var settingLines = new List<string>();
            using (var reader = new StreamReader(SettingsPath))
            {
                while (!reader.EndOfStream)
                {
                    settingLines.Add(reader.ReadLine());
                }
            }
            foreach (string settingLine in settingLines)
            {
                if (settingLine == null)
                    throw new Exception("WOOTUFUQUE");
                string line = settingLine.ToLower();
                string[] setting = line.Split('=');
                if (setting.Length != 2)
                    continue;
                setting[0] = setting[0].Trim();
                setting[1] = setting[1].Trim();

                try
                {
                    string[] vector;
                    switch (setting[0])
                    {
                        case "uiborders":
                            settings.UITiles = setting[1];
                            break;
                        case "uigamezonebegin":
                            vector = setting[1].Split(',');
                            settings.UIGamezoneBegin = new Vector(int.Parse(vector[0]), int.Parse(vector[1]));
                            break;
                        case "uigamezoneend":
                            vector = setting[1].Split(',');
                            settings.UIGamezoneEnd = new Vector(int.Parse(vector[0]), int.Parse(vector[1]));
                            break;
                        case "drawzoneend":
                            vector = setting[1].Split(',');
                            settings.DrawzoneEnd = new Vector(int.Parse(vector[0]), int.Parse(vector[1]));
                            break;
                        default: //todo log
                            break;
                    }
                }
                catch (Exception)
                {
                    //todo log?
                    throw;
                }
            }
            Setting = settings;
        }

        public void SaveSettings()
        {
            throw new NotImplementedException();
        }

        public void LoadSettings(string path)
        {
            SettingsPath = path;
            LoadSettings();
        }
    }
}