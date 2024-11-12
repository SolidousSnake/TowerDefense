using System;
using System.Collections.Generic;
using _Project.Code.Core.AssetManagement;
using _Project.Code.Config;
using _Project.Code.Presenter;
using _Project.Code.Services.Wallet;
using _Project.Code.Utils;
using UnityEngine.UI;
using UnityEngine;
using VContainer;
using UniRx;

namespace _Project.Code.UI.View
{
    public class TowerShopView : BaseUI
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _parentContent;

        [Inject] private IAssetProvider _assetProvider;
        [Inject] private ConfigProvider _configProvider;
        [Inject] private WalletService _walletService;

        private readonly List<TowerShopItem> _shopItems = new();

        private readonly CompositeDisposable _cd = new();
        
        public event Action<TowerConfig> PurchaseButtonPressed;

        public void Initialize(TowerShopPresenter presenter)
        {
            _closeButton.OnClickAsObservable().Subscribe(_ => presenter.Hide()).AddTo(_cd);
            
            var shopItem = _assetProvider.Load<TowerShopItem>(AssetPath.Prefab.TowerShopItem);
            var shopColor = _configProvider.GetSingle<TowerShopColors>();

            foreach (var config in _configProvider.GetSingle<LevelConfig>().TowersList)
            {
                var item = Instantiate(shopItem, _parentContent);
                item.Initialize(shopColor, config, _walletService);
                _shopItems.Add(item);
                
                item.PurchaseButtonPressed += Invoke;
            }
        }

        private void Invoke(TowerConfig config)
        {
            PurchaseButtonPressed?.Invoke(config);
        }

        private void OnDestroy()
        {
            _cd.Dispose();
            
            foreach (var item in _shopItems) 
                item.PurchaseButtonPressed -= PurchaseButtonPressed;
        }
    }
}
