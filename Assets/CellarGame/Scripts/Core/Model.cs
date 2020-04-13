namespace CellarGame
{
    public class Model : ICleanUpable
    {
        #region Fields

        public readonly Entity Entity = default;

        #endregion


        #region ClassLifeCycle

        protected Model(Entity entity) => Entity = entity;

        #endregion


        #region ICleanUpable

        public virtual void CleanUp() { }

        #endregion
    }
}