using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper.Exceptions
{
    public class GameLoadConfigException : FileNotFoundException
    {
        public GameLoadConfigException(string fileName) : base($"Could not find the configuration file for the game instance. The expected location was {fileName}.", fileName)
        {
        }
    }
}
