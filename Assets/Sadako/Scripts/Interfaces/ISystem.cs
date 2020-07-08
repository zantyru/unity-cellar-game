using System;


namespace Sadako
{
    public interface ISystem
    {
        #region Properties

        Type ArchetypeType { get; }

        #endregion


        #region Methods

        void Process(Archetype archetype, float deltaTime);

        #endregion
    }
}
