using System;
using UnityEngine;


namespace CellarGame
{
    public abstract class Model<TEntityInterface> : ScriptableObject, IModel
        where TEntityInterface : class, IEntityInterface
    {
        #region Properties

        public abstract Type ModelType { get; }
        public IEntity Entity { get; private set; } = default;
        public TEntityInterface EntityInterface { get => Entity as TEntityInterface; }

        #endregion


        #region Methods

        public void SetOnwer(IEntity entity) => Entity = entity;

        #endregion
    }
}