using System.Collections.Generic;


namespace CellarGame
{
    public abstract class System : IExecutable, IHavePowerSwitch
    {
        #region Fields

        private bool _isEnabled = false;
        private readonly HashSet<Entity> _suitableEntities = new HashSet<Entity>();

        #endregion


        #region Properties IHavePowerSwitch

        public bool IsEnabled => _isEnabled;

        #endregion


        #region IHavePowerSwitch

        public virtual void On() => _isEnabled = true;

        public virtual void Off() => _isEnabled = false;

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (!IsEnabled)
            {
                return;
            }

            foreach (Entity entity in _suitableEntities)
            {
                Process(entity);
            }
        }

        #endregion


        #region Methods

        public bool TryRegisterEntity(Entity entity) => Filter(entity) ? _suitableEntities.Add(entity) : false;

        public bool TryUnregisterEntity(Entity entity) => _suitableEntities.Remove(entity);

        protected abstract bool Filter(Entity entity);

        protected abstract void Process(Entity entity);

        #endregion
    }
}