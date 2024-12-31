using System;
using System.Collections.Generic;
using _Project.Code.Core.AssetManagement;
using _Project.Code.Data.Config;
using _Project.Code.Presenter;
using _Project.Code.Services.Wallet;
using _Project.Code.Utils;
using UnityEngine;
using VContainer;
using UniRx;
using UnityEngine.UI;

namespace _Project.Code.UI.View
{
    public class TowerShopView : MoveableUI
    {
        [Header("Common")]
        [SerializeField] private Button _openButton;
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
            Close();
            _openButton.OnClickAsObservable().Subscribe(_ => presenter.Show()).AddTo(_cd);
            _closeButton.OnClickAsObservable().Subscribe(_ => presenter.Hide()).AddTo(_cd);

            var shopItem = _assetProvider.Load<TowerShopItem>(AssetPath.Prefab.TowerShopItem);
            var shopColor = _configProvider.GetSingle<ShopColors>();

            foreach (var config in _configProvider.GetSingle<LevelConfig>().TowersList)
            {
                var item = Instantiate(shopItem, _parentContent);
                item.Initialize(shopColor, config, _walletService);
                _shopItems.Add(item);

                item.PurchaseButtonPressed += Purchased;
            }
        }

        public override void Open()
        {
            base.Open();
            _openButton.Hide();
        }

        public override void Close()
        {
            base.Close();
            _openButton.Show();
        }

        private void Purchased(TowerConfig config)
        {
            Close();
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