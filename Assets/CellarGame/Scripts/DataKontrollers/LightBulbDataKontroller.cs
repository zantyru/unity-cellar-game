using UnityEngine;
using Sadako;


namespace CellarGame
{
    public sealed class LightBulbDataKontroller : DataKontroller
    {
        #region Fields

        public readonly bool IsEmittingLightAtStart = true;
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
        
        public LightBulbDataKontroller(GameObject owner) : base(owner)
        {
            _light = owner.GetComponent<Light>();
            SetDefaultData();
        }

        #endregion


        #region Methods

        public override void SetDefaultData()
        {
            IsEmittingLight = IsEmittingLightAtStart;
        }

        #endregion
    }
}