using System.Collections.Generic;
using UnityEngine;


namespace CellarGame
{
    public sealed class WorldProcessor : MonoBehaviour, IInitializable
    {
        #region Fields

        private ISystem[] _systems;
        private static readonly Queue<Model> _registeringModelsQueue = new Queue<Model>();

        #endregion


        #region UnityMethods

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Initialize();
        }

        private void Update()
        {
            foreach (var system in _systems)
            {
                system.Execute();
            }
        }

        #endregion


        #region IInitializable

        public void Initialize()
        {
            _systems = new ISystem[]
            {
                new FlashLightSystem(),
            };

            foreach (var system in _systems)
            {
                if (system is IInitializable initialization)
                {
                    initialization.Initialize();
                }

                foreach (Model model in _registeringModelsQueue)
                {
                    system.AddModel(model);
                }

                system.On();
            }

            _registeringModelsQueue.Clear();
        }

        #endregion


        #region Methods

        public static void RegisterModel(Model model)
        {
            _registeringModelsQueue.Enqueue(model);
        }

        #endregion
    }
}