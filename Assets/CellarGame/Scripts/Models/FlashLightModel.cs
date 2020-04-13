using System;
using UnityEngine;


namespace CellarGame
{
    public sealed class FlashLightModel : Model
    {
        #region Fields

        private float _battaryChargeAtStart = 50.0f;
        private float _batteryChargeMax = 100.0f;
        private float _rechargingSpeed = 2.0f;
        private float _dischargingSpeed = 2.5f;
        private Light _light;

        #endregion


        #region Properties

        public bool IsEmittingLight { get; private set; }
        public float BatteryChargeAtStart => _battaryChargeAtStart;
        public float BatteryChargeCurrent { get; private set; }
        public float BatteryChargeMax => _batteryChargeMax;

        #endregion


        #region ClassLifeCycle
        
        public FlashLightModel(Entity entity) : base(entity)
        {
            _light = Entity.FetchComponent<Light>(out bool isJustCreated);

            if (isJustCreated)
            {
                //@REFACTORME Move this data to ScriptableObject
                _light.enabled = false;
                _light.type = LightType.Spot;
                _light.range = 10.0f;
                _light.spotAngle = 30.0f;
                _light.color = new Color(1.0f, 0.924f, 0.651f);
                _light.lightmapBakeType = LightmapBakeType.Realtime;
                _light.intensity = 1.0f;
                _light.bounceIntensity = 0.0f;
                _light.shadows = LightShadows.Soft;
                _light.shadowStrength = 1.0f;
                _light.shadowResolution = UnityEngine.Rendering.LightShadowResolution.FromQualitySettings;
                _light.shadowBias = 0.05f;
                _light.shadowNormalBias = 0.4f;
                _light.shadowNearPlane = 0.2f;
                _light.renderMode = LightRenderMode.Auto;
                _light.cullingMask = LayerMask.NameToLayer("Everything");
            }
            
            BatteryChargeCurrent = _battaryChargeAtStart;
            IsEmittingLight = _light.enabled;
        }

        #endregion


        #region Methods

        public void Switch(FlashLightStateType value = FlashLightStateType.None)
        {
            switch (value)
            {
                case FlashLightStateType.None:
                    if (IsEmittingLight)
                    {
                        Switch(FlashLightStateType.Off);
                    }
                    else if (BatteryChargeCurrent > 0.0f)
                    {
                        Switch(FlashLightStateType.On);
                    }
                    break;

                case FlashLightStateType.On:
                    IsEmittingLight = true;
                    break;
                
                case FlashLightStateType.Off:
                    IsEmittingLight = false;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            _light.enabled = IsEmittingLight;
        }

        public bool DischargeBattery()
        {
            bool isHappened = false;

            if (BatteryChargeCurrent > 0.0f)
            {
                BatteryChargeCurrent -= _dischargingSpeed * Time.deltaTime;
                BatteryChargeCurrent = Mathf.Max(BatteryChargeCurrent, 0.0f);
                isHappened = true;
            }

            return isHappened;
        }

        public bool RechargeBattery()
        {
            bool isHappened = false;

            if (BatteryChargeCurrent < _batteryChargeMax)
            {
                BatteryChargeCurrent += _rechargingSpeed * Time.deltaTime;
                BatteryChargeCurrent = Mathf.Min(BatteryChargeCurrent, _batteryChargeMax);
                isHappened = true;
            }

            return isHappened;
        }

        #endregion
    }
}