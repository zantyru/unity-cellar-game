using UnityEngine;


namespace CellarGame
{
    public abstract class Model : ScriptableObject
    {
        #region Properties

        public IEntity Entity { get; private set; } = default;

        #endregion


        #region Methods

        public void SetOwner(IEntity entity) => Entity = entity;

        #endregion
    }
}