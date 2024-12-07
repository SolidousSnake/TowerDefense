using _Project.Code.Services.Tower;
using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace _Project.Code.UI.View
{
    public class TowerPlacementView : MoveableUI
    {
        [Title("Common")]
        [SerializeField] private Button _placeButton;
        [SerializeField] private Button _rotateButton;
        [SerializeField] private Button _cancelButton;

         private TowerPlacementService _placementService;

        public void Initialize(TowerPlacementService placementService)
        {
            _placementService = placementService;
            _cancelButton.OnClickAsObservable().Subscribe(_ => StopPlacement()).AddTo(this);
            _placeButton.OnClickAsObservable().Subscribe(_ => PlaceBuilding()).AddTo(this);
            _rotateButton.OnClickAsObservable().Subscribe(_ => _placementService.RotatePreviewBuilding()).AddTo(this);
        }

        private void PlaceBuilding()
        {
            _placementService.PlaceBuilding();
        }

        private void StopPlacement()
        {
            _placementService.StopPlacement();
            Close();
        }
    }
}