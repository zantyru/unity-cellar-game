namespace CellarGame
{
    public interface ISystem : IExecutable, IHavePowerSwitch
    {
        bool AddModel(Model model);
        bool RemoveModel(Model model);
    }
}