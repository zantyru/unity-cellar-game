using System;


namespace CellarGame
{
    public interface IModel
    {
        #region Fields

        Type ModelType { get; }
        IEntity Entity { get; }

        #endregion


        #region Methods

        void SetOnwer(IEntity entity);

        #endregion
    }
}