using UnityEngine;

namespace Game.Character.States
{
    public class GroundedState : BaseState
    {
        [SerializeField] private float drag = 10f;
        [SerializeField] private float magnitudeSmooth = 0.1f;

        private readonly int _aInputMagnitude = Animator.StringToHash("input_magnitude");
        private float _magnitudeSmoothDump;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            character.Motion.SetDrag(drag);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var input = character.Input.MovementInput;
            if (character.Ground.IsGrounded)
                character.Motion.SetMovementInput(input);
            animator.SetFloat(_aInputMagnitude, input.magnitude, _magnitudeSmoothDump, magnitudeSmooth * Time.deltaTime);

            if (character.Input.JumpInput && character.Ground.IsGrounded)
                character.Motion.TriggerJump();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetFloat(_aInputMagnitude, 0f);
            character.Motion.SetMovementInput(Vector2.zero);
        }
    }
}