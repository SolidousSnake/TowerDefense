using System.Collections.Generic;
using _Project.Code.Config;
using _Project.Code.Core.AssetManagement;
using _Project.Code.Gameplay.Repository;
using _Project.Code.Services.Input;
using _Project.Code.UI.Indicator;
using _Project.Code.UI.View;
using _Project.Code.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Services.TowerPlacement
{
    public class TowerPlacementService : ITickable
    {
        private readonly List<TowerConfig> _towers;
        private readonly List<GameObject> _placedTowers;
        
        [Inject] private readonly IInputService _inputService;
        [Inject] private readonly Camera _sceneCamera;
        [Inject] private readonly Grid _grid;
        [Inject] private readonly GridView _gridView;

        private readonly LevelConfig _levelConfig;
        private readonly CursorIndicator _cellIndicator;
        private readonly Renderer _previewRenderer;

        private readonly GridRepository _placementRepository;

        private Vector3 _lastPosition;
        private int _selectedTowerIndex;

        public TowerPlacementService(IAssetProvider assetProvider, ConfigProvider configProvider)
        {
            _cellIndicator = Object.Instantiate(assetProvider.Load<CursorIndicator>(AssetPath.Prefab.CursorIndicator));
            _levelConfig = configProvider.GetSingleImmediately<LevelConfig>(AssetPath.Config.LevelConfig);
            _towers = _levelConfig.TowersList;
            _placedTowers = new List<GameObject>();

            _selectedTowerIndex = -1;

            _placementRepository = new GridRepository();
            _previewRenderer = _cellIndicator.GetComponentInChildren<Renderer>();
        }

        public void Tick()
        {
            if (_selectedTowerIndex < 0)
                return;

            Vector3 mousePosition = GetPlacementPosition();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

            bool canPlace = CanPlace(gridPosition, _selectedTowerIndex);
            _previewRenderer.material.color = canPlace ? Color.cyan : Color.red;

            _cellIndicator.transform.position = _grid.CellToWorld(gridPosition);
        }

        public void StartPlacement(TowerConfig config)
        {
            _selectedTowerIndex = _towers.IndexOf(config);

            _gridView.Show();
            _cellIndicator.Show();
            _inputService.OnClicked += PlaceTower;
            _inputService.OnExit += StopPlacement;
        }

        private void StopPlacement()
        {
            _selectedTowerIndex = -1;

            _gridView.Hide();
            _cellIndicator.Hide();
            _inputService.OnClicked -= PlaceTower;
            _inputService.OnExit -= StopPlacement;
        }

        private void PlaceTower()
        {
            if (_inputService.IsPointerOverUI())
                return;

            Vector3 mousePosition = GetPlacementPosition();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

            bool canPlace = CanPlace(gridPosition, _selectedTowerIndex);

            if (canPlace == false)
                return;

            var tower = Object.Instantiate(_towers[_selectedTowerIndex].Prefab);
            tower.transform.position = _grid.CellToWorld(gridPosition);
            _placedTowers.Add(tower);
            _placementRepository.Add(gridPosition
                , _levelConfig.TowersList[_selectedTowerIndex].Size
                , tower
                , _placedTowers.Count - 1);
        }

        public void RemoveTowerAt(Vector3Int gridPosition)
        {
            int towerIndex = _placementRepository.GetObjectIndex(gridPosition);
            
            if (towerIndex == -1)
                return;

            GameObject towerToRemove = _placedTowers[towerIndex];
            
            _placedTowers.RemoveAt(towerIndex);
            Object.Destroy(towerToRemove);

            _placementRepository.Remove(gridPosition);
        }
        
        private bool CanPlace(Vector3Int gridPosition, int selectedTowerIndex) => 
            _placementRepository.CanPlace(gridPosition, _levelConfig.TowersList[selectedTowerIndex].Size);

        private Vector3 GetPlacementPosition()
        {
            Vector3 selectedPosition = _inputService.GetSelectedPosition();
            selectedPosition.z = _sceneCamera.nearClipPlane;
            Ray ray = _sceneCamera.ScreenPointToRay(selectedPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100, _levelConfig.PlacementLayer))
                _lastPosition = hit.point;

            return _lastPosition;
        }
    }
}