using UnityEngine;


namespace CellarGame
{
    public abstract class MechanikaData
    {
        #region Fields

        public readonly GameObject Owner = default;

        #endregion


        #region ClassLifeCycle
        
        protected MechanikaData(GameObject owner) => Owner = owner;

        #endregion
    }
}