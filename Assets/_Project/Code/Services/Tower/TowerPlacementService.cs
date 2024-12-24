using _Project.Code.Config;
using _Project.Code.Gameplay.Repository;
using _Project.Code.Gameplay.Tower;
using _Project.Code.Services.Wallet;
using _Project.Code.UI.View;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace _Project.Code.Services.Tower
{
    public class TowerPlacementService : ITickable
    {
        private readonly Camera _camera;
        private readonly WalletService _walletService;
        private readonly TowerOperationService _towerOperationService;
        private readonly EnemyRepository _enemyRepository;
        private readonly BuildingRepository _buildingRepository;
        private readonly TowerPlacementView _view;
        private readonly LayerMask _placementLayer;

        private Building _previewBuilding;
        private TowerConfig _currentConfig;

        public TowerPlacementService(
            WalletService walletService,
            TowerOperationService towerOperationService,
            TowerPlacementView view,
            EnemyRepository enemyRepository,
            BuildingRepository buildingRepository,
            LayerMask placementLayer)
        {
            _walletService = walletService;
            _towerOperationService = towerOperationService;
            _enemyRepository = enemyRepository;
            _buildingRepository = buildingRepository;
            _view = view;
            _placementLayer = placementLayer;
            _camera = Camera.main;

            _view.Initialize(this);
            _view.Close();
        }

        public void StartPlacement(TowerConfig config)
        {
            StopPlacement();
            MovePreviewBuilding(Vector3.zero);
            
            _currentConfig = config;
            _previewBuilding = Object.Instantiate(config.Prefab);
            _view.Open();
        }

        public void RotatePreviewBuilding() => _previewBuilding?.Rotate();

        public void PlaceBuilding()
        {
            if (_previewBuilding is null || !TryPlaceBuilding(out var gridPosition))
                return;

            FinalizePlacement(gridPosition);
        }

        public void StopPlacement()
        {
            if (_previewBuilding is null)
                return;

            Object.Destroy(_previewBuilding.gameObject);
            _previewBuilding = null;
            _view.Close();
        }

        public void Tick()
        {
            var mousePosition = Input.mousePosition;
            MovePreviewBuilding(mousePosition);
            HandlePlacementInput(mousePosition);
        }
        
        private void MovePreviewBuilding(Vector3 mousePosition)
        {
            if (_previewBuilding is null)
                return;
        
            if (TryGetGridPosition(mousePosition, out var gridPosition))
            {
                bool canPlace = _buildingRepository.IsPositionFree(_previewBuilding, gridPosition);
                _previewBuilding.transform.position = new Vector3(gridPosition.x, 0f, gridPosition.y);
                _previewBuilding.SetTransparent(canPlace);
            }
        }

        private void HandlePlacementInput(Vector3 mousePosition)
        {
            if (_previewBuilding is not null)
                return;

            if (!Input.GetMouseButtonDown(0) || !TryGetGridPosition(mousePosition, out var gridPosition))
                return;

            _buildingRepository.TryGetBuilding(gridPosition, out var building);

            if (building is null)
                return;
            _towerOperationService.Show(building);
        }
        
        private bool TryPlaceBuilding(out Vector2Int gridPosition)
        {
            gridPosition = default;
        
            if (_previewBuilding is null)
                return false;
        
            var position = _previewBuilding.transform.position;
            gridPosition = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
        
            return _buildingRepository.IsPositionFree(_previewBuilding, gridPosition);
        }

        private void FinalizePlacement(Vector2Int gridPosition)
        {
            _buildingRepository.Add(_previewBuilding, gridPosition);
            _walletService.ReduceGameplayCoins(_currentConfig.Price);
            _previewBuilding.ResetColor();
            _previewBuilding.GetComponent<TowerFacade>().Initialize(_currentConfig, _enemyRepository, gridPosition);
            _previewBuilding = null;
            _view.Close();
        }

        private bool TryGetGridPosition(Vector3 mousePosition, out Vector2Int gridPosition)
        {
            gridPosition = default;

            var ray = _camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _placementLayer))
            {
                var worldPosition = hit.point;
                gridPosition = new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.z));
                return true;
            }

            return false;
        }
    }
}