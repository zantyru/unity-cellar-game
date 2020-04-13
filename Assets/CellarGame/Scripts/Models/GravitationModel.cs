using System;
using UnityEngine;


namespace CellarGame
{
    public sealed class GravitationModel : Model
    {
        #region Fields

        [SerializeField] private float _magnitudeGrowSpeed = 9.8f; // 30.0f
		[SerializeField] private float _magnitudeIfGrounded = 0.1f;
        [SerializeField] private Vector3 _direction = Vector3.down;
        private float _magnitude;

        #endregion


        #region Properties

        public float MagnitudeGrowSpeed => _magnitudeGrowSpeed;
        public float MagnitudeIfGrounded => _magnitudeIfGrounded;
        public Vector3 Direction => _direction;

        #endregion


        #region ClassLifeCycle
        
        public GravitationModel(Entity entity) : base(entity) { }

        #endregion


        #region UnityMethods

        private void Awake() => _direction.Normalize();

        #endregion


        #region Methods

        public Vector3 GetGravityForce(bool isGrounded)
		{
            if (isGrounded)
            {
                _magnitude = _magnitudeIfGrounded;
            }
            else
            {
                _magnitude += _magnitudeGrowSpeed * Time.deltaTime;
            }

            return _magnitude * _direction;
		}

        #endregion
    }
}