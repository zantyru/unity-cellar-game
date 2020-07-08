using Sadako;


namespace CellarGame
{
    public sealed class CellarGameWorld : World
    {
        #region Methods

        public override void Initialize()
        {
            UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;

            //Systems
            RegisterSystem(new PlayerSystem());
            RegisterSystem(new FlashLightSystem());
        }

        #endregion
    }
}