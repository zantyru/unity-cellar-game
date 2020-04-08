using UnityEngine;


namespace CellarGame
{
    public sealed class FlashLightSystem : System<FlashLightModel, ILightEntityInterface>
    {
        #region Methods

        protected override void Process(FlashLightModel flashLightModel)
        {
            var flashLightInterface = flashLightModel.EntityInterface;
            
            if(flashLightModel.IsEmittingLight)
            {
                if (!flashLightModel.DischargeBattery())
                {
                    flashLightModel.Switch(FlashLightStateType.Off);
                }
            }
            else
            {
                flashLightModel.RechargeBattery();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                flashLightModel.Switch();
            }

            flashLightInterface.Light.enabled = flashLightModel.IsEmittingLight;
        }

        #endregion
    }
}