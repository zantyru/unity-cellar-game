using UnityEngine;
using Sadako;


namespace CellarGame
{
    public sealed class PlayerSystem : System<PlayerArchetype>
    {
        #region Methods

        public override void Process(Archetype archetype, float deltaTime)
        {
            var player = archetype.ResolveDataKontroller<PlayerDataKontroller>();
            var playerFlashLightInteraction = player.FlashLightArchetype.ResolveDataKontroller<InteractionDataKontroller<FlashLightInteractionType>>();
            var playerCharacter = archetype.ResolveDataKontroller<CharacterMotionDataKontroller>();

            if (Input.GetKeyDown(KeyCode.F))
            {
                playerFlashLightInteraction.Add(FlashLightInteractionType.ToggleUsed);
            }

            playerCharacter.Move(deltaTime);
        }

        #endregion
    }
}