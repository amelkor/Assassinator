using UnityEngine;

namespace Game.Character
{
    public class FirstPersonRotator : MonoBehaviour, ICharacterRotation
    {
        [SerializeField] private Transform yaw;
        [SerializeField] private Transform pitch;
        [SerializeField] private float speed = 1f;
        [SerializeField] private float lookAxisVerticalMin = -90f;
        [SerializeField] private float lookAxisVerticalMax = 70f;
        
        private Vector2 _input;
        
        public void SetInput(Vector2 input) => _input = input;

        private void Update()
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Rotate();
        }

        private void Rotate()
        {
            var pitchRotation = Quaternion.Euler(Vector3.right * (_input.y * speed * Time.deltaTime));
            pitchRotation *= pitch.localRotation;
            ClampRotationAroundXAxis(ref pitchRotation);
            pitch.localRotation = pitchRotation;
            
            var yawRotation = Quaternion.Euler(Vector3.up * (_input.x * speed * Time.deltaTime));
            yaw.rotation *= yawRotation;
        }
        
        private void ClampRotationAroundXAxis(ref Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            var angle = Mathf.Clamp(2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x), lookAxisVerticalMin, lookAxisVerticalMax);
            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angle);
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(yaw)
            {
                var t = yaw;
                var p = t.position;
                UnityEditor.Handles.color = Color.yellow;
                UnityEditor.Handles.ArrowHandleCap(0, p, yaw.rotation, 0.5f, EventType.Repaint);
            }

            if (pitch)
            {
                var t = pitch;
                var p = t.position;
                //Gizmos.DrawLine(p, t.forward + p);
                UnityEditor.Handles.color = Color.yellow;
                UnityEditor.Handles.ArrowHandleCap(0, p, pitch.rotation, 0.5f, EventType.Repaint);
            }
        }
#endif
    }
}