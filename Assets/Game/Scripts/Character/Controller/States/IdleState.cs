using UnityEngine;

namespace Game.Character.States
{
    public class IdleState : BaseState
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            TurnOnKinematic();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            TurnOffKinematic();
        }

        private void TurnOnKinematic() => character.Motion.SetKinematic(true);

        private void TurnOffKinematic() => character.Motion.SetKinematic(false);
    }
}