using Bedroom.Minesweeper.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper.Exceptions
{
    public class GameLoadException : Exception
    {
        public GameLibrary Library { get; set; }

        public GameLoadException(GameLibrary gameLibrary) : base($"Could not load given assembly.")
        {
            Library = gameLibrary;
        }
    }
}
