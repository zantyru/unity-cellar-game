using UnityEngine;


namespace CellarGame
{
    public sealed class WorldEntity : MonoBehaviour
    {
        #region Fields

        private World _world;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _world = new World();

            Entity[] entities = Resources.FindObjectsOfTypeAll<Entity>();
            foreach (Entity entity in entities)
            {
                entity.Initialize(_world);
            }
        }

        private void Update() => _world.Process(Time.deltaTime);

        #endregion
    }
}