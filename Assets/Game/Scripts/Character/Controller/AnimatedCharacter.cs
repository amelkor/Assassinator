using UnityEngine;

namespace Game.Character
{
    [RequireComponent(typeof(Animator))]
    public sealed class AnimatedCharacter : MonoBehaviour, IAnimatedCharacter
    {
        [SerializeField] private GroundDetection ground;
        [SerializeField] private CharacterMotion motion;
        [SerializeField] private FirstPersonRotator rotation;
        [SerializeField] private PlayerCharacterInput input;
        
        public ICharacterGroundDetection Ground => ground;
        public ICharacterMotion Motion => motion;
        public ICharacterRotation Rotation => rotation;
        public ICharacterInput Input => input;
    }
}