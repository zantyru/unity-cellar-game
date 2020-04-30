using System;
using System.Runtime.Serialization;


namespace CellarGame
{
    [Serializable]
    public class CellarGameBaseException : Exception
    {
        #region ClassLifeCycle

        public CellarGameBaseException()
        { }

        public CellarGameBaseException(string message)
            : base(message)
        { }

        public CellarGameBaseException(string message, Exception inner)
            : base(message, inner)
        { }

        protected CellarGameBaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #endregion
    }
}