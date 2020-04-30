using System;


namespace CellarGame
{
    public sealed class FlashLightSystem : ISystem
    {
        #region Fields

        private LightBulbMechanikaHandler _lightBulbHandler = default;
        private EnergyBankMechanikaHandler _energyBankHandler = default;
        
        #endregion


        #region Properties ISystem

        public Type ArchetypeType { get => typeof(FlashLightArchetype); }

        #endregion


        #region ISystem

        public void Initialize(Archetype archetype)
        {
            FlashLightArchetype flashLight = (FlashLightArchetype)archetype;
            _lightBulbHandler = (LightBulbMechanikaHandler)flashLight.GetMechanikaHandler(typeof(LightBulbMechanikaHandler));
            _energyBankHandler = (EnergyBankMechanikaHandler)flashLight.GetMechanikaHandler(typeof(EnergyBankMechanikaHandler));
        }

        public void Process(Entity entity, float deltaTime)
        {
            LightBulbMechanikaData lightBulb = _lightBulbHandler.Handle(entity);
            EnergyBankMechanikaData energyBank = _energyBankHandler.Handle(entity);

            if(lightBulb.IsEmittingLight)
            {
                
                if (!_energyBankHandler.Discharge(energyBank, deltaTime))
                {
                    lightBulb.IsEmittingLight = false;
                }
            }
            else
            {
                _energyBankHandler.Recharge(energyBank, deltaTime);
            }

            //@TODO Input system
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F))
            {
                SwitchLightBulb(lightBulb);
            }
        }

        #endregion


        #region Methods

        private void SwitchLightBulb(LightBulbMechanikaData lightBulb)
        {
            lightBulb.IsEmittingLight = !lightBulb.IsEmittingLight;
            
            /*
            switch (lightBulb.IsEmittingLight)
            {
                case true:
                    lightBulb.IsEmittingLight = false;
                    break;

                case false:
                    lightBulb.IsEmittingLight = true;
                    break;
            }
            */
        }

        #endregion
    }
}