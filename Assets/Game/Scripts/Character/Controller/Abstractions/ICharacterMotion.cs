using UnityEngine;

namespace Game.Character
{
    /// <summary>
    /// Provides controls for a character's movement.
    /// </summary>
    public interface ICharacterMotion
    {
        Vector2 LocalVelocity { get; }
        
        /// <summary>
        /// Update movement input vector
        /// </summary>
        /// <param name="input">Normalized input vector.</param>
        void SetMovementInput(Vector2 input);

        /// <summary>
        /// <list type="table">
        /// <item>x: forward speed</item>
        /// <item>y: strafe speed</item>
        /// <item>z: backwards speed</item>
        /// </list>
        /// </summary>
        void SetSpeedScale(Vector3 speed);

        /// <summary>
        /// Update character's rigidbody drag value.
        /// </summary>
        /// <param name="value">Drag value.</param>
        void SetDrag(float value);
        
        /// <summary>
        /// Makes the character jump.
        /// </summary>
        void TriggerJump();
        
        void AddJumpForce(float force);
        
        /// <summary>
        /// Makes the character turn kinematic on/off.
        /// </summary>
        /// <param name="enable">Whether enable or disable kinematics.</param>
        void SetKinematic(bool enable);
    }
}