using System;
using UnityEngine;

namespace Game.Character
{
    public class MaintainPositionYOffset : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 axis = new (1f, -1f, 1f);
        [SerializeField] private float damp = 5f;

        private Transform _transform;
        private Vector3 _offset;
        
        private void Awake()
        {
            _transform = transform;
            _offset = target.position - _transform.position;
        }

        private void LateUpdate()
        {
            var transformPosition = target.position + target.forward * _offset.y;
            _transform.position = Vector3.Lerp(_transform.position, transformPosition, damp * Time.deltaTime);
        }
    }
}