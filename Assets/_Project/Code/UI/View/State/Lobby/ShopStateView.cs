using _Project.Code.Data.Config;
using _Project.Code.Data.Enum;
using _Project.Code.Data.PersistentProgress;
using _Project.Code.Services.MenuShop;
using _Project.Code.Services.SaveLoad;
using _Project.Code.Services.Wallet;
using _Project.Code.UI.UIButton;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.View.State.Lobby
{
    public class ShopStateView : MoveableUI
    {
        [BoxGroup("Tab")] [SerializeField] private Button _assaultPanelButton;
        [BoxGroup("Tab")] [SerializeField] private Button _demomanPanelButton;
        [BoxGroup("Tab")] [SerializeField] private Button _sniperPanelButton;
        [BoxGroup("Content")] [SerializeField] private UpgradeButton _damageUpgradeButton;
        [BoxGroup("Content")] [SerializeField] private UpgradeButton _rangeUpgradeButton;
        [BoxGroup("Content")] [SerializeField] private UpgradeButton _fireRateUpgradeButton;
        
        [BoxGroup("Debug")] [SerializeField] private Button _addMoneyButton;
        [BoxGroup("Debug")] [SerializeField] private Button _reduceMoneyButton;
        [BoxGroup("Debug")] [SerializeField] private Button _resetProgressButton;

        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private WalletService _walletService;
        
        private MenuShopService _shopService;
        private TowerType _towerType;
        public TowerType TowerType => _towerType;

        void ResetData()
        {
            var progress = _saveLoadService.Load();
            progress.UpgradeData = new UpgradeData();
            _saveLoadService.Save(progress);
        }
        
        public void Initialize(MenuShopService shopService, Star starPrefab, MenuShopConfig config, int moneyAmount, ShopColors shopColors)
        {
            _shopService = shopService;

            _addMoneyButton.OnClickAsObservable().Subscribe(_ => _walletService.AddMenuCoins(100)).AddTo(this);
            _reduceMoneyButton.OnClickAsObservable().Subscribe(_ => _walletService.ReduceMenuCoins(20)).AddTo(this);
            
            _damageUpgradeButton.Initialize(starPrefab, config.Items[_towerType].DamageUpgradePrices.Length, shopColors, shopService, _walletService, this);
            _rangeUpgradeButton.Initialize(starPrefab, config.Items[_towerType].RangeUpgradePrices.Length, shopColors, shopService, _walletService, this);
            _fireRateUpgradeButton.Initialize(starPrefab, config.Items[_towerType].FireRateUpgradePrices.Length, shopColors, shopService, _walletService, this);

            SetTowerType(TowerType.Assault);

            _damageUpgradeButton.Pressed += Purchase;
            _rangeUpgradeButton.Pressed += Purchase;
            _fireRateUpgradeButton.Pressed += Purchase;
            
            _assaultPanelButton.OnClickAsObservable().Subscribe(_ => SetTowerType(TowerType.Assault)).AddTo(this);
            _demomanPanelButton.OnClickAsObservable().Subscribe(_ => SetTowerType(TowerType.Demoman)).AddTo(this);
            _sniperPanelButton.OnClickAsObservable().Subscribe(_ => SetTowerType(TowerType.Sniper)).AddTo(this);
            _resetProgressButton.OnClickAsObservable().Subscribe(_ => ResetData()).AddTo(this);
        }

        public void UpdateUI(TowerUpgradeData upgradeData)
        {
            _damageUpgradeButton.UpdateUI(upgradeData.DamageLevel, _shopService.GetUpgradePrice(_towerType, UpgradeType.Damage));
            _rangeUpgradeButton.UpdateUI(upgradeData.RangeLevel, _shopService.GetUpgradePrice(_towerType, UpgradeType.Range));
            _fireRateUpgradeButton.UpdateUI(upgradeData.FireRateLevel, _shopService.GetUpgradePrice(_towerType, UpgradeType.FireRate));
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
                _walletService.AddMenuCoins(100);
            if(Input.GetKeyDown(KeyCode.O))
                _walletService.ReduceMenuCoins(20);
        }

        private void SetTowerType(TowerType towerType)
        {
            _towerType = towerType;
            var upgradeData = _shopService.GetTowerUpgradeData(towerType);

            UpdateUI(upgradeData);
        }

        private void Purchase(UpgradeType upgradeType) => _shopService.Purchase(upgradeType, _towerType);

        private void OnDestroy()
        {
            _damageUpgradeButton.Pressed -= Purchase;
            _rangeUpgradeButton.Pressed -= Purchase;
            _fireRateUpgradeButton.Pressed -= Purchase;
        }
    }
}