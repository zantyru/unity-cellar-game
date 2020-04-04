using UnityEngine;


namespace CellarGame
{
    [RequireComponent(typeof(Light))]
    public sealed class FlashLightEntity : Entity, IFlashLightEntityInterface
    {
        #region Fields

        private Light _light;

        #endregion


        #region Properties IFlashLightModel

        public Light Light { get => _light; }

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
            AddModel<FlashLightModel, IFlashLightEntityInterface>();

            var flashLightModel = GetModel<FlashLightModel, IFlashLightEntityInterface>();
            if (_light.enabled)
            {
                flashLightModel.Switch(FlashLightStateType.On);
            }
            else
            {
                flashLightModel.Switch(FlashLightStateType.Off);
            }
        }

        #endregion
    }
}