using System;
using System.Runtime.Serialization;

namespace Bedroom.Minesweeper.Exceptions
{
    [Serializable]
    public class AssetLoadException<T> : Exception
    {
        #region Public Constructors

        public AssetLoadException(string message) : base(message)
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected AssetLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}