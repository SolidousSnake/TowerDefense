using System;
using System.Collections.Generic;
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
        private TowerPlacementService _placementService;

        public void Initialize(IEnumerable<TowerConfig> towers, TowerPlacementService service)
        {
            _placementService = service;
            _view.Initialize(towers);
            _view.PurchaseButtonPressed += Buy;
        }

        private void Buy(TowerConfig config)
        {
            _placementService.StartPlacement(config);
            if (_walletService.GameplayCoins.Value >= config.Price)
            {
                
            }
        }

        public void Show() => _view.Show();
        public void Hide() => _view.Hide();
        
        public void Dispose() => _view.PurchaseButtonPressed -= Buy;
    }
}