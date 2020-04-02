using UnityEngine;


namespace CellarGame
{
    [RequireComponent(typeof(Light))]
    public sealed class FlashLightEntity : Entity
    {
        #region Fields

        private Light _light;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _light = GetComponent<Light>();
        }

        #endregion


        #region Methods

        public override void Initialize()
        {
            AddModel<FlashLightModel>();

            GetModel<FlashLightModel>().SetLightComponent(_light);
        }

        #endregion
    }
}