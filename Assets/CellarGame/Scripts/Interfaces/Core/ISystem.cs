using System;


namespace CellarGame
{
    public interface ISystem : IHavePowerSwitch
    {
        #region Properties

        Type ModelType { get; }

        #endregion


        #region Methods

        void Process(object boxedModel);

        #endregion
    }
}