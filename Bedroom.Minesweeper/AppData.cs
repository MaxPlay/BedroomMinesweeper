using System;
using System.IO;
using System.Reflection;

namespace Bedroom.Minesweeper
{
    public static class AppData
    {
        #region Private Fields

        private const string APPDATA_FOLDER = "Bedroom Minesweeper";

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// The location of the executable
        /// </summary>
        public static string ApplicationFolder { get; private set; }

        /// <summary>
        /// The location of the folder in appdata that is used by the application
        /// </summary>
        public static string DataFolder { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public static void Load()
        {
            ApplicationFolder = Assembly.GetEntryAssembly().Location;
            DataFolder = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName, "LocalLow", APPDATA_FOLDER);
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);
        }

        #endregion Public Methods
    }
}