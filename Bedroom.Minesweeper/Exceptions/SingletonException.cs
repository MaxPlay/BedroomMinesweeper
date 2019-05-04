using System;
using System.Runtime.Serialization;

namespace Bedroom.Minesweeper.Exceptions
{
    [Serializable]
    internal class SingletonException<T> : Exception
    {
        public SingletonException() : base($"An instance of {nameof(T)} already exists.")
        {
        }
        
        protected SingletonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}