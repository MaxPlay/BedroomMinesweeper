using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper
{
    public static class AppData
    {
        private const string APPDATA_FOLDER = "Bedroom Minesweeper";

        /// <summary>
        /// The location of the executable
        /// </summary>
        public static string ApplicationFolder { get; private set; }

        /// <summary>
        /// The location of the folder in appdata that is used by the application
        /// </summary>
        public static string DataFolder { get; private set; }

        static AppData()
        {
            ApplicationFolder = Assembly.GetEntryAssembly().Location;
            DataFolder = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName, "LocalLow", APPDATA_FOLDER);
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);
        }
    }
}
