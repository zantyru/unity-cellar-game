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
        Transform Transform { get; }

        #endregion


        #region Methods

        T GetModel<T>() where T : Model;

        #endregion
    }
}