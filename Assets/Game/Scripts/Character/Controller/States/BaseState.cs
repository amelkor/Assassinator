using UnityEngine;

namespace Game.Character.States
{
    public abstract class BaseState : StateMachineBehaviour
    {
        protected IAnimatedCharacter character;
        private bool _isInitialized;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                if (!animator.TryGetComponent(out character))
                    throw new MissingComponentException(nameof(IAnimatedCharacter) + " component is not attached to the Animator");
            }
        }
    }
}