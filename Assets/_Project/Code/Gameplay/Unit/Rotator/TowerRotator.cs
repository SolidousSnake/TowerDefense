using UnityEngine;

namespace _Project.Code.Gameplay.Unit.Rotator
{
    public class TowerRotator
    {
        private readonly Transform _transform;
        private readonly Transform _rotationPart;

        public TowerRotator(Transform transform, Transform rotationPart)
        {
            _transform = transform;
            _rotationPart = rotationPart;
        }
        
        public void Rotate(Vector3 position)
        {
            // var direction = position - _transform.position;
            // var lookRotation = Quaternion.LookRotation(direction);
            // var rotation = Quaternion.Lerp(_rotationPart.rotation
                // , lookRotation, Time.deltaTime * 10000f).eulerAngles;
            // _rotationPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            
            Vector3 direction = position - _rotationPart.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _rotationPart.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
        }
        
        public bool IsAlignedWith(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - _rotationPart.position).normalized;
            float angle = Vector3.Angle(_rotationPart.forward, direction);
            return angle < 5f;
        }
    }
}