namespace CellarGame
{
    public sealed class EnergyBankMechanikaHandler : MechanikaHandler<EnergyBankMechanikaData>
    {
        #region Fields

        public readonly float InitialEnergyLevel = 50.0f;
        public readonly float MaximumEnergyLevel = 100.0f;
        public readonly float RechargingSpeed = 2.0f;
        public readonly float DischargingSpeed = 2.5f;

        #endregion


        #region ClassLifeCycle

        public EnergyBankMechanikaHandler(World world) : base(world) { }
        
        #endregion


        #region Methods
        
        public override void InitializeData(MechanikaData mechanikaData)
        {
            EnergyBankMechanikaData energyBank = (EnergyBankMechanikaData)mechanikaData;
            energyBank.CurrentEnergyLevel = InitialEnergyLevel;
        }

        public bool Discharge(EnergyBankMechanikaData energyBank, float deltaTime)
        {
            bool isHappened = false;

            if (energyBank.CurrentEnergyLevel > 0.0f)
            {
                energyBank.CurrentEnergyLevel -= DischargingSpeed * deltaTime;
                if (energyBank.CurrentEnergyLevel < 0.0f)
                {
                    energyBank.CurrentEnergyLevel = 0.0f;
                }
                isHappened = true;
            }

            return isHappened;
        }

        public bool Recharge(EnergyBankMechanikaData energyBank, float deltaTime)
        {
            bool isHappened = false;

            if (energyBank.CurrentEnergyLevel < MaximumEnergyLevel)
            {
                energyBank.CurrentEnergyLevel += RechargingSpeed * deltaTime;
                if (energyBank.CurrentEnergyLevel > MaximumEnergyLevel)
                {
                    energyBank.CurrentEnergyLevel = MaximumEnergyLevel;
                }
                isHappened = true;
            }

            return isHappened;
        }

        #endregion
    }
}