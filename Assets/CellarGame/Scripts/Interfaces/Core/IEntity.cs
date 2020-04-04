using System;
using UnityEngine;


namespace CellarGame
{
    public interface IEntity : IHavePowerSwitch
    {
        #region Properties
        
        bool IsVisible { get; set; }
        int Layer { get; set; }
        string Name { get; set; }
        Color Color { get; set; }
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        Vector3 Scale { get; set; }

        #endregion


        #region Methods

        TModel GetModel<TModel, TEntityInterface>()
            where TModel : Model<TEntityInterface>
            where TEntityInterface : class, IEntityInterface;
        
        TModel GetModel<TModel>()
            where TModel : IModel;
        
        IModel GetModel(Type modelType);

        #endregion
    }
}