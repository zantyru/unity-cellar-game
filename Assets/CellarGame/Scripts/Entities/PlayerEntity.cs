using UnityEngine;


namespace CellarGame
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerEntity : Entity, IHaveGroundChecker
    {
        #region Fields

        private CharacterController _characterController;

        #endregion


        #region Properties IHaveGroundChecker

        public bool IsGrounded => _characterController.isGrounded;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _characterController = GetComponent<CharacterController>();
        }

        #endregion


        #region Methods

        public override void Initialize()
        {
            AddModel<GravitationModel, IEntityInterface>();
            AddModel<MotionModel, IEntityInterface>();
        }

        #endregion
    }
}