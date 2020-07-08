using System;


namespace Sadako
{
    [Serializable]
    public sealed class AttemptToUsingUnregisteredObjectException : CellarGameBaseException
    {
        #region ClassLifeCycle

        public AttemptToUsingUnregisteredObjectException()
        { }

        public AttemptToUsingUnregisteredObjectException(string message)
            : base(message)
        { }

        public AttemptToUsingUnregisteredObjectException(string message, Exception inner)
            : base(message, inner)
        { }
        
        #endregion
    }
}