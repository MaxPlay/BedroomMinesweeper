using System;
using System.Runtime.Serialization;

namespace Bedroom.Minesweeper.Exceptions
{
    [Serializable]
    public class AssetLoadException<T> : Exception
    {
        public AssetLoadException(string message) : base(message)
        {
        }
        
        protected AssetLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}