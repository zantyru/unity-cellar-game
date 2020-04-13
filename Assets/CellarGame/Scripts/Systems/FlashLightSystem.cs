using UnityEngine;


namespace CellarGame
{
    public sealed class FlashLightSystem : System
    {
        #region Methods

        protected override bool Filter(Entity entity) => entity.HasModel<FlashLightModel>();

        protected override void Process(Entity entity)
        {
            FlashLightModel flashLightModel = entity.GetModel<FlashLightModel>();

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

            //@TODO Create a user input system.
            if (Input.GetKeyDown(KeyCode.F))
            {
                flashLightModel.Switch();
            }
        }

        #endregion
    }
}