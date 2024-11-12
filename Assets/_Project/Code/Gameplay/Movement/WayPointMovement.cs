using System;
using _Project.Code.Gameplay.Point;
using UnityEngine;

namespace _Project.Code.Gameplay.Movement
{
    public class WayPointMovement : MonoBehaviour
    {
        private WayPoint[] _waypoints;
        private float _speed = 2f;
        private int _currentWaypointIndex = 0;

        public void Initialize(float speed)
        {
            if (speed < 0)
                throw new Exception($"Speed must be positive. Received: {speed}");
            _speed = speed;
        }
        
        private void Update()
        {
            if (_currentWaypointIndex < _waypoints.Length) 
                MoveTowardsWaypoint();
        }

        private void MoveTowardsWaypoint()
        {
            Transform target = _waypoints[_currentWaypointIndex].transform;
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * (_speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.1f) 
                _currentWaypointIndex++;
        }
    }
}