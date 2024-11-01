using _Project.Code.UI.Indicator;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Code.Services.TowerPlacement
{
    public class TowerPlacementService : ITickable
    {
        private readonly Camera _sceneCamera;
        private readonly LayerMask _placementLayerMask;

        private readonly Grid _grid;
        private readonly GameObject _mouseIndicator;
        private readonly CursorIndicator _cellIndicator;

        private Vector3 _lastPosition;

        public TowerPlacementService(Camera sceneCamera, LayerMask placementLayerMask, Grid grid
            , GameObject mouseIndicator, CursorIndicator cellIndicator)
        {
            _sceneCamera = sceneCamera;
            _placementLayerMask = placementLayerMask;
            _mouseIndicator = mouseIndicator;
            _cellIndicator = cellIndicator;
            _grid = grid;
        }

        public void Tick()
        {
            Vector3 mousePosition = GetPlacementPosition();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

            _cellIndicator.transform.position = _grid.CellToWorld(gridPosition);
            _mouseIndicator.transform.position = mousePosition;
        }

        private Vector3 GetPlacementPosition()
        {
            Vector3 mousePos = UnityEngine.Input.mousePosition;
            mousePos.z = _sceneCamera.nearClipPlane;
            Ray ray = _sceneCamera.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, 100, _placementLayerMask))
                _lastPosition = hit.point;

            return _lastPosition;
        }
    }
}