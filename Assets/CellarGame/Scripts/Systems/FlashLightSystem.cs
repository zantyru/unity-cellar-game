using UnityEngine;


namespace CellarGame
{
    public sealed class FlashLightSystem : CellarGame.System<FlashLightModel> //, IInitializable
    {
        #region Fields

        // private const string FLASH_LIGHT_MODEL_PATH = "Models/FlashLightModel";
        // private FlashLightModel _flashLightModel = default;

        #endregion


        // #region IInitializable

        // public void Initialize()
        // {
        //     _flashLightModel = Resources.Load<FlashLightModel>(FLASH_LIGHT_MODEL_PATH);
        // }

        // #endregion


        #region Methods

        protected override void Process()
        {
            foreach (FlashLightModel flashLightModel in _models)
            {
                if(flashLightModel.IsEmittingLight)
                {
                    //flashLightModel.Rotate();
                    
                    if (flashLightModel.DischargeBattery())
                    {
                    }
                    else
                    {
                        flashLightModel.Switch(FlashLightStateType.Off);
                    }
                }
                else
                {
                    if (flashLightModel.RechargeBattery())
                    {
                    }   
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    flashLightModel.Switch();
                }
            }
        }

        // public void Switch()
        // {
        //     if (_flashLightModel.IsEmittingLight)
        //     {
        //         _flashLightModel.Switch(FlashLightStateType.Off);
        //     }
        //     else if (_flashLightModel.BatteryChargeCurrent > 0.0f)
        //     {
        //         _flashLightModel.Switch(FlashLightStateType.On);
        //     }
        //     Debug.Log("FLS Switch");
        // }

        #endregion
    }
}