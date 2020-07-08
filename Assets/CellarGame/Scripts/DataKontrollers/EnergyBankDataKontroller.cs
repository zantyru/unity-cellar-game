using UnityEngine;
using Sadako;


namespace CellarGame
{
    public sealed class EnergyBankDataKontroller : DataKontroller
    {
        #region Fields

        public readonly float InitialEnergyLevel = 50.0f;
        public readonly float MaximumEnergyLevel = 100.0f;
        public readonly float RechargingSpeed = 2.0f;
        public readonly float DischargingSpeed = 2.5f;

        #endregion


        #region Properties

        public float CurrentEnergyLevel { get; private set; } = 0.0f;

        #endregion


        #region ClassLifeCycle
        
        public EnergyBankDataKontroller(GameObject owner) : base(owner)
        {
            SetDefaultData();
        }

        #endregion


        #region Methods

        public override void SetDefaultData()
        {
            CurrentEnergyLevel = InitialEnergyLevel;
        }

        public bool Discharge(float deltaTime)
        {
            bool isHappened = false;

            if (CurrentEnergyLevel > 0.0f)
            {
                CurrentEnergyLevel -= DischargingSpeed * deltaTime;
                if (CurrentEnergyLevel < 0.0f)
                {
                    CurrentEnergyLevel = 0.0f;
                }
                isHappened = true;
            }

            return isHappened;
        }

        public bool Recharge(float deltaTime)
        {
            bool isHappened = false;

            if (CurrentEnergyLevel < MaximumEnergyLevel)
            {
                CurrentEnergyLevel += RechargingSpeed * deltaTime;
                if (CurrentEnergyLevel > MaximumEnergyLevel)
                {
                    CurrentEnergyLevel = MaximumEnergyLevel;
                }
                isHappened = true;
            }

            return isHappened;
        }

        #endregion
    }
}