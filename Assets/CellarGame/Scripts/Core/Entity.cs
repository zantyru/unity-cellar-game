using System;
using System.Collections.Generic;
using UnityEngine;


namespace CellarGame
{
    public abstract class Entity : MonoBehaviour, IMechanikaDataResolver
    {
        #region Fields

        protected World _world;
        private readonly Dictionary<Type, MechanikaData> _data = new Dictionary<Type, MechanikaData>();

        #endregion


        #region IMechanikaDataResolver

        public T Resolve<T>() where T : MechanikaData
        {
            T mechanikaData = null;

            if (_data.TryGetValue(typeof(T), out var foundData))
            {
                mechanikaData = (T)foundData;
            }
            else
            {
                throw new ObjectNotFoundException(
                    $"{this} does not have instance of '{typeof(T)}'."
                );
            }

            return mechanikaData;
        }

        #endregion


        #region UnityMethods

        private void Start()
        {
            IEnumerable<Archetype> archetypes = _world.GetEntityArchetypes(this);
            IEnumerable<IMechanikaHandler> handlers;

            foreach (Archetype archetype in archetypes)
            {
                handlers = archetype.GetMechanikaHandlers();
                foreach (IMechanikaHandler handler in handlers)
                {
                    //@NOTE See https://stackoverflow.com/questions/752/how-to-create-a-new-object-instance-from-a-type

                    Type mechanikaDataType = handler.MechanikaDataType;
                    if (!_data.ContainsKey(mechanikaDataType))
                    {
                        // SO SLOOOOW! But only at start.
                        MechanikaData data = (MechanikaData)Activator.CreateInstance(mechanikaDataType, (object)gameObject);
                        _data.Add(mechanikaDataType, data);
                        handler.InitializeData(data);
                    }
                }
            }
        }

        #endregion


        #region Methods

        public virtual void Initialize(World world) => _world = world;

        #endregion
    }
}