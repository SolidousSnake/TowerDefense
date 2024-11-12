using System.Collections.Generic;
using _Project.Code.Gameplay.Tower;
using UnityEngine;

namespace _Project.Code.Gameplay.Repository
{
    public class BuildingRepository
    {
        private readonly Dictionary<Vector2Int, Building> _buildingGrid = new();

        public void Add(Building building, Vector2Int position)
        {
            for (int x = 0; x < building.Size.x; x++)
                for (int z = 0; z < building.Size.y; z++)
                    _buildingGrid[position + new Vector2Int(x, z)] = building;
        }

        public void RemoveBuilding(Vector2Int position)
        {
            if (!_buildingGrid.TryGetValue(position, out Building building)) 
                return;
            
            for (int x = 0; x < building.Size.x; x++)
                for (int z = 0; z < building.Size.y; z++)
                    _buildingGrid.Remove(position + new Vector2Int(x, z));
        }

        public bool IsPositionFree(Building building, Vector2Int position)
        {
            for (int x = 0; x < building.Size.x; x++)
                for (int z = 0; z < building.Size.y; z++)
                    if (_buildingGrid.ContainsKey(position + new Vector2Int(x, z)))
                        return false;
            
            return true;
        }

        public void Clear() => _buildingGrid.Clear();
    }
}