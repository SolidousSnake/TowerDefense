using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Unit.Rotator
{
    public class TowerRotator
    {
        private readonly Transform _transform;
        private readonly Transform _rotationPart;

        private float _rotationSpeed;

        public TowerRotator(Transform transform, Transform rotationPart)
        {
            _transform = transform;
            _rotationPart = rotationPart;
        }

        public void SetRotationSpeed(float speed)
        {
            if (speed < 0)
                throw new ArgumentException($"Speed can not be negative. Received: {speed}");
            _rotationSpeed = speed;
        }
        
        public void Rotate(Vector3 position)
        {
            var direction = position - _transform.position;
            var lookRotation = Quaternion.LookRotation(direction);
            var rotation = Quaternion.Lerp(_rotationPart.rotation
                , lookRotation, Time.deltaTime * _rotationSpeed).eulerAngles;
            
            _rotationPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        
        public bool IsAlignedWith(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - _rotationPart.position).normalized;
            float angle = Vector3.Angle(_rotationPart.forward, direction);
            return angle < 5f; 
        }
    }
}