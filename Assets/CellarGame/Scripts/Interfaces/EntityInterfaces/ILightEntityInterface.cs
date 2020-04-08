using UnityEngine;


namespace CellarGame
{
    public interface ILightEntityInterface : IEntityInterface
    {
        Light Light { get; }
    }
}