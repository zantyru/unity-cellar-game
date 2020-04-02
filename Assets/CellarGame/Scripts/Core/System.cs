using System.Collections.Generic;


namespace CellarGame
{
    public abstract class System<T> : ISystem where T : Model
    {
        #region Fields

        private bool _isEnabled = false;
        protected readonly HashSet<T> _models = new HashSet<T>();

        #endregion


        #region Properties ISystem

        public bool IsEnabled => _isEnabled;

        #endregion


        #region ISystem

        public void Execute()
        {
            if (!_isEnabled)
            {
                return;
            }

            Process();
        }

        public virtual void On() => _isEnabled = true;

        public virtual void Off() => _isEnabled = false;

        public bool AddModel(Model model)
        {
            bool result = false;

            if (model is T adaptedModel)
            {
                result = _models.Add(adaptedModel);
            }

            return result;
        }

        public bool RemoveModel(Model model)
        {
            bool result = false;

            if (model is T adaptedModel)
            {
                result = _models.Remove(adaptedModel);
            }

            return result;
        }

        #endregion


        #region Methods

        protected abstract void Process();

        #endregion
    }
}