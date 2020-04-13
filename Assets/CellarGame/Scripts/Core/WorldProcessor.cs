namespace CellarGame
{
    public sealed class WorldProcessor : IExecutable
    {
        #region Fields

        private readonly System[] _systems;

        #endregion


        #region ClassLifeCycle

        public WorldProcessor()
        {
            //@NOTE Add new systems here
            _systems = new System[] {
                new FlashLightSystem(),
            };

            foreach (System system in _systems)
            {
                system.On();
            }
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            foreach (System system in _systems)
            {
                system.Execute();
            }
        }
        
        #endregion


        #region Methods

        public void RegisterEntity(Entity entity)
        {
            foreach (System system in _systems)
            {
                system.TryRegisterEntity(entity);
            }
        }

        public void UnregisterEntity(Entity entity)
        {
            foreach (System system in _systems)
            {
                system.TryUnregisterEntity(entity);
            }
        }

        #endregion
    }
}