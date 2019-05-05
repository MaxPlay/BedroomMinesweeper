using System;
using System.Runtime.Serialization;

namespace Bedroom.Minesweeper.Exceptions
{
    [Serializable]
    internal class SingletonException<T> : Exception
    {
        #region Public Constructors

        public SingletonException() : base($"An instance of {nameof(T)} already exists.")
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected SingletonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}