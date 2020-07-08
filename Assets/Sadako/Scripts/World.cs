using System;
using System.Collections.Generic;
using UnityEngine;


namespace Sadako
{
    using ArchetypeType = Type;

    public abstract class World : MonoBehaviour, IInitializable
    {
        #region Fields

        private readonly List<ISystem> _systems
            = new List<ISystem>();
        private readonly Dictionary<ArchetypeType, HashSet<Archetype>> _archetypes
            = new Dictionary<ArchetypeType, HashSet<Archetype>>();

        #endregion


        #region UnityMethods

        private void Awake() => Initialize();

        private void Update() => Process(Time.deltaTime);

        #endregion


        #region IInitializable

        public abstract void Initialize();

        #endregion


        #region Methods

        public void RegisterArchetype(in Archetype archetype)
        {
            ArchetypeType archetypeType = archetype.GetType();

            if(_archetypes.TryGetValue(archetypeType, out var hashSet))
            {
                hashSet.Add(archetype);
            }
            else
            {
                hashSet = new HashSet<Archetype>();
                hashSet.Add(archetype);
                _archetypes.Add(archetypeType, hashSet);
            }
        }

        public void UnregisterArchetype(in Archetype archetype)
        {
            ArchetypeType archetypeType = archetype.GetType();

            if(_archetypes.TryGetValue(archetypeType, out var hashSet))
            {
                hashSet.Remove(archetype);
            }
        }

        protected void RegisterSystem(in ISystem system) => _systems.Add(system);

        protected void UnregisterSystem(in ISystem system) => _systems.Remove(system);

        private void Process(float deltaTime)
        {
            foreach(var system in _systems)
            {
                var hashSet = _archetypes[system.ArchetypeType];
                foreach(var archetype in hashSet)
                {
                    system.Process(archetype, deltaTime);
                }
            }
        }

        #endregion
    }
}
