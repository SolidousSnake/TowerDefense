using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Data
{
    public class PlacementData
    {
        public List<Vector3Int> OccupiedPositions { get; set; }
        public GameObject GameObject { get; set; }
        public int ObjectIndex { get; set; }

        public PlacementData()
        {
            
        }

        public PlacementData(List<Vector3Int> occupiedPositions, GameObject gameObject, int objectIndex)
        {
            OccupiedPositions = occupiedPositions;
            GameObject = gameObject;
            ObjectIndex = objectIndex;
        }
    }

}