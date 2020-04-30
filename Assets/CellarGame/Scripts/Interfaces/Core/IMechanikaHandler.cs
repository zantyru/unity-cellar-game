using System;


namespace CellarGame
{
    public interface IMechanikaHandler
    {
        #region Properties

        Type MechanikaDataType { get; }

        #endregion


        #region Methods

        void InitializeData(MechanikaData mechanikaData);

        #endregion
    }
}