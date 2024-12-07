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
        private readonly WalletService _walletService;
        private readonly EnemyRepository _enemyRepository;
        private readonly BuildingRepository _buildingRepository;
        
        private readonly TowerPlacementView _view;
        private readonly LayerMask _placementLayer;

        private Building _previewBuilding;
        private TowerConfig _currentConfig;

        public TowerPlacementService(
            WalletService walletService
            , TowerPlacementView view
            , EnemyRepository enemyRepository
            , BuildingRepository buildingRepository
            , LayerMask placementLayer)
        {
            _view = view;
            _walletService = walletService;
            _enemyRepository = enemyRepository;
            _buildingRepository = buildingRepository;
            _placementLayer = placementLayer;

            _view.Initialize(this);
            _view.Close();
        }

        public void StartPlacement(TowerConfig config)
        {
            if (_previewBuilding is not null)
                StopPlacement();

            _currentConfig = config;
            _previewBuilding = Object.Instantiate(config.Prefab);
            _view.Open();
        }

        public void RotatePreviewBuilding() => _previewBuilding?.Rotate();

        public void PlaceBuilding()
        {
            if (_previewBuilding is null)
                return;

            var position = _previewBuilding.transform.position;
            var gridPosition = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));

            if (!_buildingRepository.IsPositionFree(_previewBuilding, gridPosition))
                return;

            _buildingRepository.Add(_previewBuilding, gridPosition);
            _walletService.ReduceGameplayCoins(_currentConfig.Price);
            _previewBuilding.ResetColor();
            _previewBuilding.GetComponent<TowerFacade>().Initialize(_currentConfig, _enemyRepository, gridPosition);
            _previewBuilding = null;
            _view.Close();
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
            var position = Input.mousePosition;
            HandleMovement(position);
        }

        private void HandleMovement(Vector3 pointPosition)
        {
            if (_previewBuilding is null)
                return;

            var ray = Camera.main.ScreenPointToRay(pointPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _placementLayer))
            {
                var worldPosition = hit.point;
                var position = new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.z));
                bool canPlace = _buildingRepository.IsPositionFree(_previewBuilding, position);

                if (_previewBuilding.transform.position == Vector3.zero)
                    _previewBuilding.transform.position = new Vector3(position.x, 0f, position.y);

                _previewBuilding.transform.position = new Vector3(position.x, 0f, position.y);
                _previewBuilding.SetTransparent(canPlace);
            }
        }
    }
}