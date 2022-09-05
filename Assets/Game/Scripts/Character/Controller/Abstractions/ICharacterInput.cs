using UnityEngine;

namespace Game.Character
{
    public interface ICharacterInput
    {
        Vector2 LookInput { get; }
        Vector2 MovementInput { get; }
        bool JumpInput { get; }
    }
}