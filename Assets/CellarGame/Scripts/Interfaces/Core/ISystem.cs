using System;


namespace CellarGame
{
    public interface ISystem
    {
        #region Properties

        Type ArchetypeType { get; }

        #endregion


        #region Methods

        void Initialize(Archetype archetype);

        void Process(Entity entity, float deltaTime);

        #endregion
    }
}