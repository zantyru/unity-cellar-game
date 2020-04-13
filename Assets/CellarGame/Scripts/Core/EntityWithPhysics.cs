using UnityEngine;


namespace CellarGame
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EntityWithPhysics : Entity
    {
        #region Fields

        private bool _isKinematic = false;

        #endregion

        #region Properties

        public bool IsKinematic
        {
            get => _isKinematic;
            set
            {
                _isKinematic = value;
                PropagateKinematics();
            }
        }
        public Rigidbody Rigidbody
        {
            get;
            private set;
        }

        #endregion


        #region UnityMethods

        public override void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody>();
            _isKinematic = Rigidbody.isKinematic;
        }

        #endregion


        #region Methods

        private void PropagateKinematics()
        {
            Rigidbody.isKinematic = _isKinematic;
            Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>(true);
            foreach (Rigidbody body in bodies)
            {
                body.isKinematic = _isKinematic;
            }
        }

        #endregion
    }
}