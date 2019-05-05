using Bedroom.Minesweeper.Levels;
using System;

namespace Bedroom.Minesweeper.Exceptions
{
    [Serializable]
    public class LevelRegisterException : Exception
    {
        #region Public Constructors

        public LevelRegisterException(string name, Level level) : base($"A level with the name {name} is already registered.")
        {
            Name = name;
            Level = level;
        }

        #endregion Public Constructors

        #region Public Properties

        public Level Level { get; set; }
        public string Name { get; set; }

        #endregion Public Properties
    }
}