namespace CellarGame
{
    public interface IHavePowerSwitch
    {
        #region Properties

        bool IsEnabled { get; }

        #endregion
        

        #region Methods

        void On();
        void Off();

        #endregion
    }
}