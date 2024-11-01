using _Project.Code.Services.TowerPlacement;
using _Project.Code.UI.Indicator;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.DiContainer
{
    public sealed class SceneDiContainer : DiContainerBase
    {
        [SerializeField] private LayerMask _placementLayerMask;
        [SerializeField] private Grid _grid;
        [SerializeField] private GameObject _mouseIndicator;
        [SerializeField] private CursorIndicator _cellIndicator;
        
        protected override void AddDependencies(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TowerPlacementService>().WithParameter(Camera.main).WithParameter(_placementLayerMask)
                .WithParameter(_grid)
                .WithParameter(_mouseIndicator)
                .WithParameter(_cellIndicator);
        }
    }
}