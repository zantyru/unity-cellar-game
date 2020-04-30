namespace CellarGame
{
    public sealed class FlashLightEntity : Entity
    {
        public override void Initialize(World world)
        {
            base.Initialize(world);
            _world.AssociateArchetypeWithEntity(this, typeof(FlashLightArchetype));
        }
    }
}