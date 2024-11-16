using _Project.Code.Gameplay.Repository;
using _Project.Code.Gameplay.Tower;
using _Project.Code.UI.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Services.TowerPlacement
{
    public class TowerPlacementService : ITickable
    {
        [Inject] private TowerPlacementView _view;

        private BuildingRepository _buildingRepository;
        private Building _previewBuilding;
        private LayerMask _placementLayer;

        public void Initialize(LayerMask layer)
        {
            _buildingRepository = new BuildingRepository();
            _placementLayer = layer;
            _view.Initialize(this);
            _view.Close();
        }

        public void StartPlacement(Building prefab)
        {
            if (_previewBuilding is not null)
                StopPlacement();

            _previewBuilding = Object.Instantiate(prefab);
            _view.Open();
        }

        public void StopPlacement()
        {
            if (_previewBuilding is null)
                return;
            
            Object.Destroy(_previewBuilding.gameObject);
            _previewBuilding = null;
            _view.Close();
        }

        public void RotatePreviewBuilding() => _previewBuilding?.Rotate();

        public void PlaceBuilding()
        {
            if (_previewBuilding is null)
                return;

            var position = _previewBuilding.transform.position;
            var gridPosition = new Vector2Int(
                Mathf.RoundToInt(position.x),
                Mathf.RoundToInt(position.z)
            );

            if (_buildingRepository.IsPositionFree(_previewBuilding, gridPosition))
            {
                _buildingRepository.Add(_previewBuilding, gridPosition);
                _previewBuilding.ResetColor();
                _previewBuilding = null;
                Debug.Log($"X: {position.x}; Z: {position.y}");
            }
        }

        public void Tick()
        {
            if (_previewBuilding is null)
                return;

            var ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _placementLayer))
            {
                var worldPosition = hit.point;
                var position = new Vector2Int(
                    Mathf.RoundToInt(worldPosition.x),
                    Mathf.RoundToInt(worldPosition.z)
                );
                bool canPlace = _buildingRepository.IsPositionFree(_previewBuilding, position);

                if (_previewBuilding.transform.position == Vector3.zero)
                    _previewBuilding.transform.position = new Vector3(position.x, -10f, position.y);

                _previewBuilding.transform.position = new Vector3(position.x, 0f, position.y);
                _previewBuilding.SetTransparent(canPlace);
            }
        }
    }
}