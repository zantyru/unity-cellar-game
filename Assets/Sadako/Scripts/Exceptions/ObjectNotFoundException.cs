using System;


namespace Sadako
{
    [Serializable]
    public sealed class ObjectNotFoundException : CellarGameBaseException
    {
        #region ClassLifeCycle

        public ObjectNotFoundException()
        { }

        public ObjectNotFoundException(string message)
            : base(message)
        { }

        public ObjectNotFoundException(string message, Exception inner)
            : base(message, inner)
        { }
        
        #endregion
    }
}