using UnityEngine;


namespace Sadako
{
    public abstract class DataKontroller
    {
        #region Fields

        protected readonly GameObject _owner = default;

        #endregion


        #region ClassLifeCycle

        protected DataKontroller(GameObject owner) => _owner = owner;

        #endregion


        #region Methods

        public virtual void SetDefaultData()
        { }

        #endregion
    }
}