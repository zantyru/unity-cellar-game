using UnityEngine;


namespace CellarGame
{
    public sealed class OrientationFollowerModel : Model
    {
        #region Fields

        private float _followingSpeed = 11.0f;
        private Vector3 _followingOffset;
        private Transform _goFollow;

        #endregion


        #region ClassLifeCycle

        public OrientationFollowerModel(Entity entity) : base(entity)
        {
            // _goFollow = Camera.main.transform;
            // _followingOffset = Entity.Transform.position - _goFollow.position;
        }

        #endregion


        #region Methods

        // Entity.Transform.position = _goFollow.position + _followingOffset;
        // Entity.Transform.rotation = _goFollow.rotation;

        public void Rotate()
        {
            Entity.Position = _goFollow.position + _followingOffset;
            Entity.Rotation = Quaternion.Lerp(
                Entity.Rotation,
                _goFollow.rotation,
                _followingSpeed * Time.deltaTime
            );
        }

        #endregion
    }
}