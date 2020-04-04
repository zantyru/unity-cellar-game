using System;
using UnityEngine;


namespace CellarGame
{
    public sealed class MotionModel : Model<IEntityInterface>
    {
        #region Fields

        public Vector3 Velocity = Vector3.zero;

        #endregion


        public override Type ModelType => typeof(MotionModel); //@DEBUG
    }
}