using System;
using _Project.Code.Config;
using _Project.Code.Services.Wallet;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.View
{
    public class TowerShopItem : BaseUI
    {
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Image _towerIcon;
        [SerializeField] private TextMeshProUGUI _towerNameLabel;
        [SerializeField] private TextMeshProUGUI _priceLabel;
        [SerializeField] private string _priceFormat = "$";

        public event Action<TowerConfig> PurchaseButtonPressed;

        public void Initialize(TowerShopColors colors, TowerConfig config, WalletService walletService)
        {
            _towerIcon.sprite = config.TowerIcon;
            _towerNameLabel.text = config.Name;
            _priceLabel.text = config.Price + _priceFormat;

            walletService.GameplayCoins.Subscribe(x => ChangeColor(colors, config, x))
                .AddTo(this);
            _purchaseButton.OnClickAsObservable().Subscribe(_ => PurchaseButtonPressed?.Invoke(config))
                .AddTo(this);
        }

        private void ChangeColor(TowerShopColors colors, TowerConfig config, int coins) => 
            _priceLabel.color = coins >= config.Price ? colors.AffordablePrice : colors.UnaffordablePrice;
    }
}