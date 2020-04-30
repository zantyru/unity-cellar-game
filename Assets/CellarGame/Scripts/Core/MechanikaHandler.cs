using System;


namespace CellarGame
{
    public abstract class MechanikaHandler<T> : IMechanikaHandler where T : MechanikaData
    {
        #region Fields

        protected World _world;

        #endregion


        #region ClassLifeCycle

        protected MechanikaHandler(World world) => _world = world; //@TODO Registartion

        #endregion


        #region Properties IMechanikaHandler

        public Type MechanikaDataType { get => typeof(T); }

        #endregion


        #region IMechanikaHandler

        public abstract void InitializeData(MechanikaData mechanikaData);

        #endregion


        #region Methods

        public T Handle(IMechanikaDataResolver resolver) => resolver.Resolve<T>();
        
        #endregion
    }
}