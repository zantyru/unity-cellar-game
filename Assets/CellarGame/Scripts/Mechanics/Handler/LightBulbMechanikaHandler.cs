namespace CellarGame
{
    public sealed class LightBulbMechanikaHandler : MechanikaHandler<LightBulbMechanikaData>
    {
        #region Fields

        public readonly bool IsEmittingLightAtStart = true;

        #endregion


        #region ClassLifeCycle

        public LightBulbMechanikaHandler(World world) : base(world) { }
        
        #endregion


        #region Methods

        public override void InitializeData(MechanikaData mechanikaData)
        {
            LightBulbMechanikaData lightBulb = (LightBulbMechanikaData)mechanikaData;
            lightBulb.IsEmittingLight = IsEmittingLightAtStart;
        }

        #endregion
    }
}