using Sadako;


namespace CellarGame
{
    using FlashLightInteractionKontroller = InteractionDataKontroller<FlashLightInteractionType>;

    public sealed class FlashLightSystem : System<FlashLightArchetype>
    {
        #region Methods

        public override void Process(Archetype archetype, float deltaTime)
        {
            var lightBulb = archetype.ResolveDataKontroller<LightBulbDataKontroller>();
            var energyBank = archetype.ResolveDataKontroller<EnergyBankDataKontroller>();
            var interaction = archetype.ResolveDataKontroller<FlashLightInteractionKontroller>();

            if(lightBulb.IsEmittingLight)
            {
                
                if (!energyBank.Discharge(deltaTime))
                {
                    lightBulb.IsEmittingLight = false;
                }
            }
            else
            {
                energyBank.Recharge(deltaTime);
            }

            if (interaction.Contains(FlashLightInteractionType.ToggleUsed))
            {
                SwitchLightBulb(lightBulb);
            }

            interaction.Clear();
        }

        private void SwitchLightBulb(LightBulbDataKontroller lightBulb)
            => lightBulb.IsEmittingLight = !lightBulb.IsEmittingLight;

        #endregion
    }
}