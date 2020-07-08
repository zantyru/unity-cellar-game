using System;
using System.Collections.Generic;
using UnityEngine;


namespace Sadako
{
    public abstract class Archetype : MonoBehaviour, IInitializable
    {
        #region Fields

        private World _world;
        private readonly Dictionary<Type, DataKontroller> _data = new Dictionary<Type, DataKontroller>();

        #endregion


        #region UnityMethods

        private void Start()
        {
            _world = FindObjectOfType<World>();
            Initialize();
            _world.RegisterArchetype(this);
        }

        #endregion


        #region IInitializable

        public abstract void Initialize();
        
        #endregion


        #region Methods
        
        public T ResolveDataKontroller<T>() where T : DataKontroller
        {
            if(!_data.ContainsKey(typeof(T)))
            {
                throw new ObjectNotFoundException(); //@TODO Have to write description.
            }

            return (T)_data[typeof(T)];
        }

        protected void AssociateDataKontroller<T>(in T data) where T : DataKontroller
        {
            if(_data.ContainsKey(typeof(T)))
            {
                throw new ObjectAlreadyRegisteredException(); //@TODO Have to write description.
            }

            _data.Add(typeof(T), data);
        }

        #endregion
    }
}