using UnityEngine;


namespace CellarGame
{
    public interface IFlashLightEntityInterface : IEntityInterface
    {
        Light Light { get; }
    }
}