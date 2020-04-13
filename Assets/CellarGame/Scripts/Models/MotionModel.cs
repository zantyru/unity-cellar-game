using System;
using UnityEngine;


namespace CellarGame
{
    public sealed class MotionModel : Model
    {
        #region Fields

        public Vector3 Velocity = Vector3.zero;

        #endregion


        #region ClassLifeCycle
        
        public MotionModel(Entity entity) : base(entity) { }

        #endregion
    }
}