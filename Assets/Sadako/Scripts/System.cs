using System;


namespace Sadako
{
    public abstract class System<T> : ISystem where T : Archetype
    {
        #region Properties ISystem

        public Type ArchetypeType { get => typeof(T); }

        #endregion


        #region ISystem

        public abstract void Process(Archetype archetype, float deltaTime);
        
        #endregion


        #region Methods
        
        protected T ResolveType(Archetype archetype) => (T)archetype;
        
        #endregion
    }
}
