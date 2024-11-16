using System;
using _Project.Code.Gameplay.Point;
using UnityEngine;

namespace _Project.Code.Gameplay.Unit.Movement
{
    public class WayPointMovement
    {
        private readonly Transform _transform;
        private readonly WayPoint[] _waypoints;

        private float _speed;
        private int _currentWaypointIndex;

        public WayPointMovement(Transform transform, WayPoint[] waypoints)
        {
            _transform = transform;
            _waypoints = waypoints;
            _currentWaypointIndex = 0;
        }

        public WayPoint GetCurrentWayPoint() =>
            _currentWaypointIndex < _waypoints.Length ? _waypoints[_currentWaypointIndex] : null;

        public void SetSpeed(float speed)
        {
            if (speed < 0)
                throw new Exception($"Speed must be positive. Received: {speed}");
            _speed = speed;
        }

        public void MoveTowardsWaypoint()
        {
            if (_currentWaypointIndex >= _waypoints.Length)
                return;
            
            Transform target = _waypoints[_currentWaypointIndex].transform;
            Vector3 direction = (target.position - _transform.position).normalized;
            
            _transform.position += direction * (_speed * Time.deltaTime);

            if (Vector3.Distance(_transform.position, target.position) < 0.01f)
                _currentWaypointIndex++;
        }
    }
}