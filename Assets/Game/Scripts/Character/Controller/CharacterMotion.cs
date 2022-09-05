using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Character
{
    /// <inheritdoc cref="ICharacterMotion"/>
    public class CharacterMotion : MonoBehaviour, ICharacterMotion
    {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private Animator animator;
        [SerializeField] private GroundDetection ground;
        [SerializeField] private Transform directionTransform;
        [SerializeField] private CapsuleCollider bodyCollider;

        [Tooltip("Triggered when character is jumped")] public UnityEvent jumped;

        private Vector2 _movementInput;
        private Vector3 _moveDirection;
        private Vector3 _speedScale;

        private readonly int _aIsGrounded = Animator.StringToHash("is_grounded");
        private readonly int _aIsJumping = Animator.StringToHash("is_jumping");

        private void FixedUpdate()
        {
            animator.SetBool(_aIsGrounded, ground.IsGrounded);
            _moveDirection = UpdateMoveDirection();

            if (_moveDirection.magnitude > 0.01f)
            {
                rigidbody.AddForce(_moveDirection, ForceMode.Acceleration);
            }
        }

        public Vector2 LocalVelocity => _movementInput != Vector2.zero ? directionTransform.InverseTransformDirection(rigidbody.velocity).Horizontal() : Vector2.zero;

        public void SetMovementInput(Vector2 input)
        {
            _movementInput.Set(input.x, input.y);
        }

        public void SetSpeedScale(Vector3 speed)
        {
            _speedScale = speed;
        }

        public void SetDrag(float value)
        {
            rigidbody.drag = value;
        }

        public void TriggerJump()
        {
            animator.SetBool(_aIsJumping, true);
        }

        public void AddJumpForce(float force)
        {
            rigidbody.AddForce(directionTransform.up * force, ForceMode.Impulse);
            jumped.Invoke();
        }
        
        public void SetKinematic(bool enable)
        {
            rigidbody.isKinematic = enable;
        }
        
        private Vector3 UpdateMoveDirection()
        {
            var forward = _speedScale.x;
            var strafe = _speedScale.y;
            var backwards = _speedScale.z;

            var speed = MathTool.ScaleInRange(_movementInput,
                new Vector2(strafe, strafe),
                new Vector2(backwards, forward));

            var t = directionTransform;

            return t.right * speed.x + t.forward * speed.y;
        }

#if UNITY_EDITOR
        [Header("Editor")]
        [SerializeField] private bool editorShowInput = true;
        [SerializeField] private bool editorShowVelocity = true;

        private void OnDrawGizmos()
        {
            if (rigidbody && directionTransform)
            {
                var t = directionTransform;
                var p = t.TransformPoint(bodyCollider.center);

                if (editorShowInput)
                {
                    if (_movementInput != Vector2.zero)
                    {
                        UnityEditor.Handles.color = Color.cyan;
                        UnityEditor.Handles.ArrowHandleCap(0, p, Quaternion.LookRotation(t.TransformDirection(_movementInput)), _movementInput.magnitude, EventType.Repaint);
                    }
                }

                if (editorShowVelocity)
                {
                    var velocity = rigidbody.velocity;
                    if (velocity != Vector3.zero)
                    {
                        var magnitude = velocity.magnitude * 0.3f;
                        UnityEditor.Handles.color = Color.magenta;
                        UnityEditor.Handles.ArrowHandleCap(0, p, Quaternion.LookRotation(velocity), magnitude, EventType.Repaint);
                        UnityEditor.Handles.Label(p + magnitude * velocity.normalized, velocity.ToString());
                    }
                }
            }
        }
#endif
    }
}