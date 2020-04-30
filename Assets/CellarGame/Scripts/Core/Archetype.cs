using System;
using System.Collections.Generic;


namespace CellarGame
{
    public abstract class Archetype : IInitializable
    {
        #region Fields

        protected World _world;

        #endregion


        #region ClassLifeCycle

        protected Archetype(World world) => _world = world;

        #endregion


        #region IInitializable

        public abstract void Initialize();
        
        #endregion


        #region Methods

        public IEnumerable<IMechanikaHandler> GetMechanikaHandlers()
            => _world.GetArchetypeMechanikaHandlers(this);

        public IMechanikaHandler GetMechanikaHandler(Type mechanikaHandlerType)
            => _world.GetArchetypeMechanikaHandler(this, mechanikaHandlerType);

        protected void AssociateMechanikaHandler(Type mechanikaHandlerType)
            => _world.AssociateMechanikaHandlerWithArchetype(this.GetType(), mechanikaHandlerType);

        #endregion
    }
}