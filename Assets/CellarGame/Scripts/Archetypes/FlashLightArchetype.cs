using Sadako;


namespace CellarGame
{
    using FlashLightInteractionKontroller = InteractionDataKontroller<FlashLightInteractionType>;

    public sealed class FlashLightArchetype : Archetype
    {
        #region Methods

        public override void Initialize()
        {
            AssociateDataKontroller<LightBulbDataKontroller>(new LightBulbDataKontroller(gameObject));
            AssociateDataKontroller<EnergyBankDataKontroller>(new EnergyBankDataKontroller(gameObject));
            AssociateDataKontroller<FlashLightInteractionKontroller>(new FlashLightInteractionKontroller(gameObject));
        }

        #endregion
    }
}