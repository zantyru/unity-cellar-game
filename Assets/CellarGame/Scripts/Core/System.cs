using System;


namespace CellarGame
{
    public abstract class System<TModel, TEntityInterface> : ISystem
        where TModel : Model<TEntityInterface>
        where TEntityInterface : class, IEntityInterface
    {
        #region Fields

        private bool _isEnabled = false;

        #endregion


        #region Properties ISystem

        public bool IsEnabled => _isEnabled;
        public Type ModelType => typeof(TModel);

        #endregion


        #region ISystem

        public virtual void On() => _isEnabled = true;

        public virtual void Off() => _isEnabled = false;

        public void Process(object boxedModel) //@REFACTORME Need use IModel
        {
            if (!IsEnabled)
            {
                return;
            }

            if (boxedModel is TModel unboxedModel)
            {
                Process(unboxedModel);
            }
        }

        #endregion


        #region Methods

        protected abstract void Process(TModel model);

        #endregion
    }
}