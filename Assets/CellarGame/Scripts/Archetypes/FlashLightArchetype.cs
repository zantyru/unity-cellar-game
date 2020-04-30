namespace CellarGame
{
    public sealed class FlashLightArchetype : Archetype
    {
        #region ClassLifeCycle

        public FlashLightArchetype(World world) : base(world) { }

        #endregion


        #region Methods

        public override void Initialize()
        {
            AssociateMechanikaHandler(typeof(LightBulbMechanikaHandler));
            AssociateMechanikaHandler(typeof(EnergyBankMechanikaHandler));
        }

        #endregion
    }
}