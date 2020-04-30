using System;


namespace CellarGame
{
    [Serializable]
    public sealed class ObjectAlreadyRegisteredException : CellarGameBaseException
    {
        #region ClassLifeCycle

        public ObjectAlreadyRegisteredException()
        { }

        public ObjectAlreadyRegisteredException(string message)
            : base(message)
        { }

        public ObjectAlreadyRegisteredException(string message, Exception inner)
            : base(message, inner)
        { }
        
        #endregion
    }
}