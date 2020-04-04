using UnityEngine;


namespace CellarGame
{
    public sealed class WorldProcessorMonoBehaviour : MonoBehaviour
    {
        #region Fields

        private readonly WorldProcessor _worldProcessor = new WorldProcessor();

        #endregion


        #region UnityMethods

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            GatherEntities();
        }

        private void Update() => _worldProcessor.Execute();

        #endregion


        #region Methods

        private void GatherEntities()
        {
            var entities = FindObjectsOfType<Entity>();

            foreach (Entity entity in entities)
            {
                _worldProcessor.RegisterEntity(entity);
            }
        }

        #endregion
    }
}