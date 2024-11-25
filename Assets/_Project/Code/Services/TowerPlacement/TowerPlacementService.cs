using _Project.Code.Config;
using _Project.Code.Gameplay.Repository;
using _Project.Code.Gameplay.Tower;
using _Project.Code.Services.Wallet;
using _Project.Code.UI.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Services.TowerPlacement
{
    public class TowerPlacementService : ITickable
    {
        [Inject] private readonly WalletService _walletService;
        [Inject] private readonly TowerPlacementView _view;
        [Inject] private readonly EnemyRepository _enemyRepository;

        private BuildingRepository _buildingRepository;
        private Building _previewBuilding;
        private LayerMask _placementLayer;

        private TowerConfig _currentConfig;

        public void Initialize(LayerMask layer)
        {
            _buildingRepository = new BuildingRepository();
            _placementLayer = layer;
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
                _walletService.ReduceGameplayCoins(_currentConfig.Price);

                _buildingRepository.Add(_previewBuilding, gridPosition);
                _previewBuilding.ResetColor();
                _previewBuilding.GetComponent<TowerFacade>().Initialize(_currentConfig, _enemyRepository);

                _previewBuilding = null;
                _view.Close();
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
                    _previewBuilding.transform.position = new Vector3(position.x, 0f, position.y);

                _previewBuilding.transform.position = new Vector3(position.x, 0f, position.y);
                _previewBuilding.SetTransparent(canPlace);
            }
        }
    }
}