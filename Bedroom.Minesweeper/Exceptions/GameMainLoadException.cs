using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper.Exceptions
{
    public class GameMainLoadException : Exception
    {
        public GameMainLoadException() : base($"The defined root type of the loaded assembly could not be found.")
        {
        }
    }
}
