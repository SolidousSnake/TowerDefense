using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Code.Config;
using _Project.Code.Data;
using UnityEngine;

namespace _Project.Code.Gameplay.Repository
{
    public class GridRepository
    {
        private readonly Dictionary<Vector3Int, PlacementData> _grid = new();

        public void Add(Vector3Int gridPosition, Vector2Int objectSize, GameObject gameObject, int objectIndex)
        {
            var positionsToOccupy = CalculateOccupiedPositions(gridPosition, objectSize);
            var data = new PlacementData(positionsToOccupy, gameObject, objectIndex);
        
            foreach (var position in positionsToOccupy)
            {
                if (_grid.ContainsKey(position))
                    throw new InvalidOperationException($"Position {position} is already occupied.");
                
                _grid[position] = data;
            }
        }

        public void Remove(Vector3Int gridPosition)
        {
            if (!_grid.TryGetValue(gridPosition, out PlacementData data))
                throw new KeyNotFoundException($"No object found at position {gridPosition} to remove.");
        
            foreach (var pos in data.OccupiedPositions) 
                _grid.Remove(pos);
        }

        public bool CanPlace(Vector3Int gridPosition, Vector2Int objectSize)
        {
            var positionsToOccupy = CalculateOccupiedPositions(gridPosition, objectSize);
            return positionsToOccupy.All(position => !_grid.ContainsKey(position));
        }

        public int GetObjectIndex(Vector3Int gridPosition)
        {
            if (!_grid.ContainsKey(gridPosition))
                return -1;
        
            return _grid[gridPosition].ObjectIndex;
        }

        private List<Vector3Int> CalculateOccupiedPositions(Vector3Int gridPosition, Vector2Int objectSize)
        {
            var positions = new List<Vector3Int>();
        
            for (int x = 0; x < objectSize.x; x++)
                for (int y = 0; y < objectSize.y; y++) 
                        positions.Add(gridPosition + new Vector3Int(x, 0, y));
        
            return positions;
        }
    }
}