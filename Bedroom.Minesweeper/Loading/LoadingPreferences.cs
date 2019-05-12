using Bedroom.Minesweeper.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bedroom.Minesweeper.Loading
{
    public class LoadingPreferences
    {
        #region Private Fields

        private const string CONFIG_FILE = "GameConfig.data";
        private static readonly Dictionary<string, ConfigValue> configKeyLookup;

        #endregion Private Fields

        #region Public Constructors

        static LoadingPreferences()
        {
            configKeyLookup = new Dictionary<string, ConfigValue>()
            {
                { "RootNamespace", ConfigValue.RootNamespace },
                { "RootType", ConfigValue.RootType },
                { "Library", ConfigValue.Library }
            };
        }

        #endregion Public Constructors

        #region Private Enums

        private enum ConfigValue
        {
            RootNamespace,
            RootType,
            Library
        }

        #endregion Private Enums

        #region Public Properties

        public string Library { get; set; }
        public string RootNamespace { get; set; }

        public string RootType { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static LoadingPreferences Create()
        {
            LoadingPreferences preferences = new LoadingPreferences();
            string configFile = Path.Combine(AppData.ApplicationFolder, CONFIG_FILE);
            if (!File.Exists(configFile))
                throw new GameLoadConfigException(configFile);

            string[] configData = File.ReadAllLines(configFile);

            foreach (string data in configData)
            {
                preferences.LoadConfig(data);
            }
            return preferences;
        }

        #endregion Public Methods

        #region Private Methods

        private void LoadConfig(string data)
        {
            // Empty lines
            if (string.IsNullOrWhiteSpace(data))
                return;

            // Comments
            if (data.Trim().StartsWith("#"))
                return;

            ConfigEntry entry = new ConfigEntry(data.Split('='), '=');
            if (configKeyLookup.ContainsKey(entry.Key))
                switch (configKeyLookup[entry.Key])
                {
                    case ConfigValue.RootNamespace:
                        RootNamespace = entry.Value;
                        break;

                    case ConfigValue.RootType:
                        RootType = entry.Value;
                        break;

                    case ConfigValue.Library:
                        Library = entry.Value;
                        break;
                }
        }

        #endregion Private Methods

        #region Private Classes

        private class ConfigEntry
        {
            #region Public Constructors

            public ConfigEntry(string[] data, char splitter)
            {
                switch (data.Length)
                {
                    case 0:
                        Key = string.Empty;
                        Value = string.Empty;
                        break;

                    case 1:
                        Key = data[0];
                        Value = string.Empty;
                        break;

                    case 2:
                        Key = data[0];
                        Value = data[1];
                        break;

                    default:
                        Key = data[0];
                        StringBuilder builder = new StringBuilder();
                        for (int i = 1; i < data.Length; i++)
                        {
                            builder.Append(data[i]).Append(splitter);
                        }
                        Value = builder.ToString();
                        break;
                }
            }

            #endregion Public Constructors

            #region Public Properties

            public string Key { get; private set; }

            public string Value { get; private set; }

            #endregion Public Properties
        }

        #endregion Private Classes
    }
}