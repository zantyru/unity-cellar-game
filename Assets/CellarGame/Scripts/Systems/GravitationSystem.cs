using UnityEngine;


namespace CellarGame
{
    public sealed class GravitationSystem : System<GravitationModel, IEntityInterface>
    {
        #region Methods

        protected override void Process(GravitationModel model)
        {
            bool isGrounded;
            Vector3 gravityForce;
            IEntity entity;
            
            isGrounded = false;
            entity = model.Entity;
            if (entity is IHaveGroundChecker groundChecker)
            {
                isGrounded = groundChecker.IsGrounded;
            }
            //gravityForce = model.GetGravityForce(isGrounded);

            //@DEBUG BEGIN
            //entity.Transform.Translate(gravityForce * Time.deltaTime);
            //MotionModel motionModel = entity.GetModel<MotionModel>();
            //motionModel.Velocity += gravityForce;
            //@DEBUG END
            
        }

        #endregion
    }
}