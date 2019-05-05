using System;

namespace Bedroom.Minesweeper.Exceptions
{
    [Serializable]
    public class LevelNotFoundException : Exception
    {
        #region Public Constructors

        public LevelNotFoundException(string name) : base($"A level with the name \"{name}\" could not be found.")
        {
            Name = name;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Name { get; }

        #endregion Public Properties
    }
}