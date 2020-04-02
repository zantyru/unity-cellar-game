using System;
using UnityEngine;


namespace CellarGame
{
    [CreateAssetMenu(
        fileName = "FlashLightModel",
        menuName = "Models/FlashLightModel",
        order = 1
    )]
    public sealed class FlashLightModel : Model
    {
        #region Fields

        //[SerializeField, Range(0.0f, 100.0f)] private float _followingSpeed = 11.0f;
        [SerializeField, Range(0.0f, 2000.0f)] private float _battaryChargeAtStart = 50.0f;
        [SerializeField, Range(0.0f, 2000.0f)] private float _batteryChargeMax = 100.0f;
        [SerializeField, Range(0.01f, 1000.0f)] private float _rechargingSpeed = 2.0f;
        [SerializeField, Range(0.01f, 1000.0f)] private float _dischargingSpeed = 2.5f;
        private Light _light;
        //private Transform _goFollow;
        //private Vector3 _followingOffset;

        #endregion


        #region Properties

        public bool IsEmittingLight { get; private set; }
        public float BatteryChargeAtStart => _battaryChargeAtStart;
        public float BatteryChargeCurrent { get; private set; }
        public float BatteryChargeMax => _batteryChargeMax;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            //_light = Entity.GetComponent<Light>();
            // _goFollow = Camera.main.transform;
            // _followingOffset = Entity.Transform.position - _goFollow.position;
            BatteryChargeCurrent = _battaryChargeAtStart;
            IsEmittingLight = false;
        }

        #endregion


        #region Methods

        public void SetLightComponent(Light light)
        {
            _light = light;
        }

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
                    _light.enabled = true;
                    // Entity.Transform.position = _goFollow.position + _followingOffset;
                    // Entity.Transform.rotation = _goFollow.rotation;
                    break;
                
                case FlashLightStateType.Off:
                    IsEmittingLight = false;
                    _light.enabled = false;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        // public void Rotate()
        // {
        //     Entity.Transform.position = _goFollow.position + _followingOffset;
        //     Entity.Transform.rotation = Quaternion.Lerp(
        //         Entity.Transform.rotation,
        //         _goFollow.rotation,
        //         _followingSpeed * Time.deltaTime
        //     );
        // }

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