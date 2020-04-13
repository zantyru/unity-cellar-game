namespace CellarGame
{
    public sealed class FlashLightEntity : Entity
    {
        #region Methods

        public override void Initialize()
        {
            AddModel<FlashLightModel>(new FlashLightModel(this));
        }

        #endregion
    }
}