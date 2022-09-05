using UnityEngine;

namespace Game.Character
{
    public interface ICharacterGroundDetection
    {
        bool IsGrounded { get; }
        float SlopeAngle { get; }
        RaycastHit Hit { get; }
        bool IsOnSteepSlope { get; }
    }
}