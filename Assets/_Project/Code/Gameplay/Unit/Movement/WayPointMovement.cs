using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Point;
using UnityEngine;

namespace _Project.Code.Gameplay.Unit.Movement
{
    public class WayPointMovement
    {
        private readonly Transform _transform;
        private readonly IReadOnlyList<WayPoint> _waypoints;

        private float _speed;
        private int _currentWaypointIndex;

        public WayPointMovement(Transform transform, IReadOnlyList<WayPoint> waypoints)
        {
            _transform = transform;
            _waypoints = waypoints;
            _currentWaypointIndex = 0;
        }

        public event Action ReachedFinalWaypoint;

        public WayPoint GetCurrentWayPoint() =>
            _currentWaypointIndex < _waypoints.Count ? _waypoints[_currentWaypointIndex] : null;

        public void SetSpeed(float speed)
        {
            if (speed < 0)
                throw new Exception($"Speed must be positive. Received: {speed}");
            _speed = speed;
        }

        public void MoveTowardsWaypoint()
        {
            if (_currentWaypointIndex >= _waypoints.Count)
                return;

            Transform target = _waypoints[_currentWaypointIndex].transform;

            _transform.position = Vector3.MoveTowards(_transform.position
                , target.position, _speed * Time.deltaTime);

            if (Vector3.Distance(_transform.position, target.position) < 0.01f) 
                _currentWaypointIndex++;

            if (_currentWaypointIndex >= _waypoints.Count)
                ReachedFinalWaypoint?.Invoke();
        }
    }
}