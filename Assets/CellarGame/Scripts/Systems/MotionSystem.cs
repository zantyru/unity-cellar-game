using UnityEngine;


namespace CellarGame
{
    public sealed class MotionSystem : System<MotionModel, IEntityInterface>
    {
        #region Methods

        protected override void Process(MotionModel model)
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