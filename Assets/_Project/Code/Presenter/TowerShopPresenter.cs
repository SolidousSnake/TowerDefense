using System;
using _Project.Code.Config;
using _Project.Code.Services.TowerPlacement;
using _Project.Code.Services.Wallet;
using _Project.Code.UI.View;
using VContainer;

namespace _Project.Code.Presenter
{
    public class TowerShopPresenter : IDisposable
    {
        [Inject] private readonly TowerShopView _view;
        [Inject] private readonly WalletService _walletService;
        [Inject] private readonly TowerPlacementService _placementService;
        
        public void Initialize()
        {
            _view.Initialize(this);
            _view.PurchaseButtonPressed += Buy;
        }

        private void Buy(TowerConfig config)
        {
            if (_walletService.GameplayCoins.Value >= config.Price)
            {
                _placementService.StartPlacement(config.Prefab);
            }
        }

        public void Show() => _view.Show();

        public void Hide()
        {
            _view.Hide();
            _placementService.StopPlacement();
        }

        public void Dispose() => _view.PurchaseButtonPressed -= Buy;
    }
}