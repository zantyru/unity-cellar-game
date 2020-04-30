using UnityEngine;


namespace CellarGame
{
    public sealed class LightBulbMechanikaData : MechanikaData
    {
        #region Fields

        private readonly Light _light = default;

        #endregion


        #region Properties
        
        public bool IsEmittingLight
        {
            get => _light.enabled;
            set => _light.enabled = value;
        }

        #endregion


        #region ClassLifeCycle
        
        public LightBulbMechanikaData(GameObject owner) : base(owner)
        {
            _light = owner.GetComponent<Light>();
        }

        #endregion
    }
}