using System;
using _Project.Code.Data.Config;
using _Project.Code.Data.Enum;
using _Project.Code.Services.MenuShop;
using _Project.Code.Services.Wallet;
using _Project.Code.UI.View.State.Lobby;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.UIButton
{
    [RequireComponent(typeof(Button))]
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _priceLabel;
        [SerializeField] private Transform _starsParent;
        [SerializeField] private UpgradeType _upgradeType;

        public Star[] _stars;
        private ShopColors _shopColors;
        private WalletService _walletService;
        
        public event Action<UpgradeType> Pressed;

        public void Initialize(Star starPrefab, int stars, ShopColors shopColors
            , MenuShopService shopService
            , WalletService walletService
            , ShopStateView shopView)
        {
            _shopColors = shopColors;
            _walletService = walletService;

            _button ??= GetComponent<Button>();
            _button.OnClickAsObservable().Subscribe(_ => Pressed?.Invoke(_upgradeType)).AddTo(this);

            walletService.MenuCoins.Subscribe(moneyAmount =>
                    ChangeColor(shopService.GetUpgradePrice(shopView.TowerType, _upgradeType), moneyAmount)).AddTo(this);

            CreateStars(starPrefab, stars);
        }

        public void UpdateUI(int statCurrentLevel, int price)
        {
            if(price > 0)
                _priceLabel.text = price + "$";
            else
                _priceLabel.text = "MAX.";

            ToggleStars(statCurrentLevel);
            ChangeColor(price, _walletService.MenuCoins.Value);
        }

        private void ChangeColor(int price, int moneyAmount) =>
            _priceLabel.color = moneyAmount >= price ? _shopColors.AffordablePrice : _shopColors.UnaffordablePrice;

        private void ToggleStars(int stars)
        {
            ClearStars();

            for (int i = 0; i < stars; i++)
                _stars[i].Toggle(true);
        }

        private void CreateStars(Star starPrefab, int count)
        {
            _stars = new Star[count];

            for (int i = 0; i < count; i++)
            {
                _stars[i] = Instantiate(starPrefab, _starsParent);
                _stars[i].Toggle(false);
            }
        }

        private void ClearStars()
        {
            foreach (var star in _stars)
                star.Toggle(false);
        }
    }
}