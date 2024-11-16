using UnityEngine;

namespace _Project.Code.Gameplay.Unit.Rotator
{
    public class EnemyRotator
    {
        private readonly Transform _transform;
        private readonly float _rotationSpeed;

        public EnemyRotator(Transform transform, float rotationSpeed)
        {
            _transform = transform;
            _rotationSpeed = rotationSpeed;
        }

        public void RotateTowards(Vector3 targetPosition)
        {
            Vector3 directionToTarget = (targetPosition - _transform.position).normalized;
            var targetRotation = Quaternion.LookRotation(directionToTarget);

            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}