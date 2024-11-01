using UnityEngine;

namespace _Project.Code.Gameplay.Weapon
{
    public class WeaponAiming : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _rotationOffset;
        
        private void Update()
        {
            AimAtTarget();
        }

        private void AimAtTarget()
        {
            Vector3 directionToTarget = _target.position - transform.position;

            float angleY = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;
            float angleX = Mathf.Asin(directionToTarget.y / directionToTarget.magnitude) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(angleX, angleY + _rotationOffset, 0);
        }

        void OnDrawGizmos()
        {
            if (_target != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, _target.position);
            }
        }
    }
}