using Sadako;


namespace CellarGame
{
    public sealed class PlayerArchetype : Archetype
    {
        #region Methods

        public override void Initialize()
        {
            AssociateDataKontroller<PlayerDataKontroller>(new PlayerDataKontroller(gameObject));
            AssociateDataKontroller<CharacterMotionDataKontroller>(new CharacterMotionDataKontroller(gameObject));
        }

        #endregion
    }
}