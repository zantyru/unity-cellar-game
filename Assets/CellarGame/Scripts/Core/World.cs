using System;
using System.Collections.Generic;


namespace CellarGame
{
    using ArchetypeType = Type;
    using HandlerType = Type;

    public class World
    {
        #region Fields

        private readonly ISystem[] _systems = default;
        private static readonly Dictionary<ArchetypeType, Archetype> _archetypes = new Dictionary<ArchetypeType, Archetype>();
        private static readonly Dictionary<HandlerType, IMechanikaHandler> _handlers = new Dictionary<HandlerType, IMechanikaHandler>();
        private static readonly Dictionary<int, Entity> _entities = new Dictionary<int, Entity>();
        private static readonly Dictionary<ArchetypeType, HashSet<HandlerType>> _handlerByArchetype = new Dictionary<ArchetypeType, HashSet<HandlerType>>();
        private static readonly Dictionary<int, HashSet<ArchetypeType>> _archetypeByEntity = new Dictionary<int, HashSet<ArchetypeType>>();
        private static readonly Dictionary<ArchetypeType, HashSet<int>> _entityByArchetype = new Dictionary<ArchetypeType, HashSet<int>>();

        #endregion


        #region ClassLifeCycle

        public World()
        {
            //@NOTE Register mechanics here.
            RegisterMechanikaHandler(new LightBulbMechanikaHandler(this));
            RegisterMechanikaHandler(new EnergyBankMechanikaHandler(this));

            //@NOTE Register archetypes here.
            Archetype[] archetypes = new Archetype[]
            {
                new FlashLightArchetype(this),
                //...
            };

            foreach (Archetype archetype in archetypes)
            {
                RegisterArchetype(archetype);
                archetype.Initialize();
            }

            //@NOTE Add system here. The order is an execution order.
            _systems = new ISystem[]
            {
                new FlashLightSystem(),
                //...
            };

            foreach (ISystem system in _systems)
            {
                if (_archetypes.TryGetValue(system.ArchetypeType, out var foundArchetype))
                {
                    system.Initialize(foundArchetype);
                }
            }
        }

        #endregion


        #region Methods

        public void Process(float deltaTime)
        {
            foreach (ISystem system in _systems)
            {
                if (_entityByArchetype.TryGetValue(system.ArchetypeType, out var entityIDs))
                {
                    foreach (int entityID in entityIDs)
                    {
                        system.Process(_entities[entityID], deltaTime);
                    }
                }
            }
        }

        public void AssociateMechanikaHandlerWithArchetype(ArchetypeType archetypeType, HandlerType handlerType)
        {
            if (!_handlers.ContainsKey(handlerType))
            {
                throw new AttemptToUsingUnregisteredObjectException(
                    $"Mechanika handler '{handlerType}' is not registered."
                );
            }

            if (!_archetypes.ContainsKey(archetypeType))
            {
                throw new AttemptToUsingUnregisteredObjectException(
                    $"Archetype '{archetypeType}' is not registered."
                );
            }

            if (!_handlerByArchetype.ContainsKey(archetypeType))
            {
                _handlerByArchetype.Add(archetypeType, new HashSet<HandlerType>());
            }

            _handlerByArchetype[archetypeType].Add(handlerType);
        }

        public void AssociateArchetypeWithEntity(Entity entity, ArchetypeType archetypeType)
        {
            if (!_archetypes.ContainsKey(archetypeType))
            {
                throw new AttemptToUsingUnregisteredObjectException(
                    $"Archetype '{archetypeType}' is not registered."
                );
            }

            int entityID = entity.GetInstanceID();
            _entities.Add(entityID, entity);

            if (!_archetypeByEntity.ContainsKey(entityID))
            {
                _archetypeByEntity.Add(entityID, new HashSet<ArchetypeType>());
            }

            _archetypeByEntity[entityID].Add(archetypeType);
            _entityByArchetype[archetypeType].Add(entityID);
        }

        public IEnumerable<Archetype> GetEntityArchetypes(Entity entity)
        {
            List<Archetype> sequence = new List<Archetype>();
            int entityID = entity.GetInstanceID();

            if (_archetypeByEntity.TryGetValue(entityID, out var archetypeTypes))
            {
                foreach (ArchetypeType archetypeType in archetypeTypes)
                {
                    sequence.Add(_archetypes[archetypeType]);
                }
            }

            return sequence;
        }

        public IEnumerable<IMechanikaHandler> GetArchetypeMechanikaHandlers(Archetype archetype)
        {
            List<IMechanikaHandler> sequence = new List<IMechanikaHandler>();

            if (_handlerByArchetype.TryGetValue(archetype.GetType(), out var handlerTypes))
            {
                foreach (HandlerType handlerType in handlerTypes)
                {
                    sequence.Add(_handlers[handlerType]);
                }
            }

            return sequence;
        }

        public IMechanikaHandler GetArchetypeMechanikaHandler(Archetype archetype, HandlerType handlerType)
        {
            IMechanikaHandler result = null;

            if (_handlerByArchetype.TryGetValue(archetype.GetType(), out var handlers))
            {
                if (handlers.Contains(handlerType))
                {
                    result = _handlers[handlerType];
                }
            }

            if (result == null)
            {
                throw new ObjectNotFoundException(
                    $"Archetype '{archetype.GetType()}' does not have mechanika handler '{handlerType}'."
                );
            }

            return result;
        }

        private void RegisterMechanikaHandler(IMechanikaHandler handler)
        {
            HandlerType handlerType = handler.GetType();

            if (_handlers.ContainsKey(handlerType))
            {
                throw new ObjectAlreadyRegisteredException(
                    $"Mechanika handler '{handlerType}' already exists."
                );
            }

            _handlers.Add(handlerType, handler);
        }

        private void RegisterArchetype(Archetype archetype)
        {
            ArchetypeType archetypeType = archetype.GetType();

            if (_archetypes.ContainsKey(archetypeType))
            {
                throw new ObjectAlreadyRegisteredException(
                    $"Archetype '{archetypeType}' already exists."
                );
            }

            _archetypes.Add(archetypeType, archetype);
            _entityByArchetype.Add(archetypeType, new HashSet<int>());
        }

        #endregion
    }
}