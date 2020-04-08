using UnityEngine;


namespace CellarGame
{
    [RequireComponent(typeof(Light))]
    public sealed class FlashLightEntity : Entity, ILightEntityInterface
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
            AddModel<FlashLightModel, ILightEntityInterface>();
            SyncModelDataWithEntityState();
        }

        private void SyncModelDataWithEntityState()
        {
            var flashLightModel = GetModel<FlashLightModel, ILightEntityInterface>();

            flashLightModel.Switch(
                _light.enabled ? FlashLightStateType.On : FlashLightStateType.Off
            );
        }

        #endregion
    }
}