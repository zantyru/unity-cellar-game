using System;
using System.Collections.Generic;


namespace CellarGame
{
    public sealed class WorldProcessor : IExecutable
    {
        #region Fields

        private readonly Dictionary<Type, ISystem> _systems = new Dictionary<Type, ISystem>();
        private readonly Dictionary<Type, HashSet<IModel>> _models = new Dictionary<Type, HashSet<IModel>>();

        #endregion


        #region ClassLifeCycle

        public WorldProcessor()
        {
            //@NOTE Add new systems here
            ISystem[] systems = {
                new FlashLightSystem(),
            };

            Type modelType;
            foreach (ISystem system in systems)
            {
                modelType = system.ModelType;
                _systems[modelType] = system;
                system.On();
            }
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            Type modelType;
            ISystem system;

            foreach (var record in _systems)
            {
                modelType = record.Key;
                system = record.Value;
                
                if (_models.TryGetValue(modelType, out var setOfModels))
                {
                    foreach (IModel model in setOfModels)
                    {
                        system.Process(model);
                    }
                }
            }
        }
        
        #endregion


        #region Methods

        public void RegisterEntity(IEntity entity)
        {
            IModel model;
            foreach (Type modelType in _systems.Keys)
            {
                model = entity.GetModel(modelType);

                if (model == null)
                {
                    continue;
                }

                RegisterModel(modelType, model);
            }
        }

        public void UnregisterEntity(IEntity entity)
        {
            //@TODO
            throw new NotImplementedException();
        }

        private void RegisterModel(Type modelType, IModel model)
        {
            if (_models.TryGetValue(modelType, out var hashSet))
            {
                hashSet.Add(model);
            }
            else
            {
                _models[modelType] = new HashSet<IModel>
                {
                    model,
                };
            }
        }

        private void UnregisterModel(Type modelType, IModel model)
        {
            //@TODO
            throw new NotImplementedException();
        }

        #endregion
    }
}