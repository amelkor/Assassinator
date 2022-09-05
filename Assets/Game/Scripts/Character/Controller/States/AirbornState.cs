using UnityEngine;

namespace Game.Character.States
{
    public class AirbornState : BaseState
    {
        [SerializeField] private float drag = 0.1f;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            character.Motion.SetDrag(drag);
        }
    }
}