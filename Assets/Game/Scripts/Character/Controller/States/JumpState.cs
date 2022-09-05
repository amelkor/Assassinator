using UnityEngine;

namespace Game.Character.States
{
    public class JumpState : BaseState
    {
        [SerializeField] private float drag = 0.1f;
        [SerializeField] private float jumpForce = 450f;

        private readonly int _aIsJumping = Animator.StringToHash("is_jumping");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            
            character.Motion.AddJumpForce(jumpForce);
            character.Motion.SetDrag(drag);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(_aIsJumping, false);
        }
    }
}