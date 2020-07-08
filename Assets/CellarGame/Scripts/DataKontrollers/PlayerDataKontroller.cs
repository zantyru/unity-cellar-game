using UnityEngine;
using Sadako;


namespace CellarGame
{
    public class PlayerDataKontroller : DataKontroller
    {
        #region Fields

        public readonly FlashLightArchetype FlashLightArchetype = default;

        #endregion


        #region ClassLifeCycle
        
        public PlayerDataKontroller(GameObject owner) : base(owner)
        {
            FlashLightArchetype = owner.GetComponentInChildren<FlashLightArchetype>();
        }

        #endregion
    }
}