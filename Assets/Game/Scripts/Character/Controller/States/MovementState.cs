using System;
using UnityEngine;

namespace Game.Character.States
{
    public class MovementState : BaseState
    {
        [SerializeField] private float forwardSpeed = 50f;
        [SerializeField] private float strafeSpeed = 45f;
        [SerializeField] private float backwardsSpeed = 40;
        [SerializeField] private float speedSmooth = 10f;
        [SerializeField] private float magnitudeSmooth = 10f;

        private readonly int _aVerticalSpeed = Animator.StringToHash("vertical_speed");
        private readonly int _aHorizontalSpeed = Animator.StringToHash("horizontal_speed");
        private readonly int _aSpeedmagnitude = Animator.StringToHash("speed_magnitude");

        private float _dampedX;
        private float _dampedY;
        private float _dampedMagnitude;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            character.Motion.SetSpeedScale(new Vector3(forwardSpeed, strafeSpeed, backwardsSpeed));

            var velocity = character.Motion.LocalVelocity.normalized;
            var magnitude = Mathf.Abs(character.Motion.LocalVelocity.magnitude);

            _dampedX = Mathf.Lerp(_dampedX, velocity.x, speedSmooth * Time.deltaTime);
            _dampedY = Mathf.Lerp(_dampedY, velocity.y, speedSmooth * Time.deltaTime);
            _dampedMagnitude = Mathf.Lerp(_dampedMagnitude, magnitude, magnitudeSmooth * Time.deltaTime);
            
            animator.SetFloat(_aHorizontalSpeed, MathF.Round(_dampedX, 2, MidpointRounding.AwayFromZero));
            animator.SetFloat(_aVerticalSpeed, MathF.Round(_dampedY, 2, MidpointRounding.AwayFromZero));
            animator.SetFloat(_aSpeedmagnitude, MathF.Round(_dampedMagnitude, 2, MidpointRounding.AwayFromZero));
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            character.Motion.SetSpeedScale(Vector3.zero);
        }
    }
}