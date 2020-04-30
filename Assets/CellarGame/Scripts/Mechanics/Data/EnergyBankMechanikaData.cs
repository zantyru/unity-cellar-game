using UnityEngine;


namespace CellarGame
{
    public sealed class EnergyBankMechanikaData : MechanikaData
    {
        #region Fields

        public float CurrentEnergyLevel = 0.0f;

        #endregion


        #region ClassLifeCycle
        
        public EnergyBankMechanikaData(GameObject owner) : base(owner)
        { }

        #endregion
    }
}