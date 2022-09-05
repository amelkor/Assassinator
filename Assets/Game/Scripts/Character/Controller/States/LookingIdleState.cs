using UnityEngine;

namespace Game.Character.States
{
    public class LookingIdleState : BaseState
    {
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            character.Rotation.SetInput(character.Input.LookInput);
        }
    }
}