using UnityEngine;


namespace CellarGame
{
    public sealed class MotionSystem : System
    {
        #region Methods

        protected override bool Filter(Entity entity)
        {
            return false;
        }

        protected override void Process(Entity entity)
        {
            // IEntity entity;
            
            // entity = model.Entity;
            // //@DEBUG BEGIN
            // //entity.Transform.Translate(gravityForce * Time.deltaTime);
            // if (entity.Transform.TryGetComponent<CharacterController>(out var character))
            // {
            //     character.Move(model.Velocity * Time.deltaTime);
            // }
            // //@DEBUG END
        }

        #endregion
    }
}