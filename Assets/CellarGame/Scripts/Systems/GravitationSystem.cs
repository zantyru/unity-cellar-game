using UnityEngine;


namespace CellarGame
{
    public sealed class GravitationSystem : System
    {
        #region Methods

        protected override bool Filter(Entity entity)
        {
            return false;
        }

        protected override void Process(Entity entity)
        {
            
        }

        #endregion
    }
}