namespace CellarGame
{
    public interface IMechanikaDataResolver
    {
        T Resolve<T>() where T : MechanikaData;
    }
}