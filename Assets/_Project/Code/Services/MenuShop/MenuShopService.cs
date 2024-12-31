using System;
using _Project.Code.Core.AssetManagement;
using _Project.Code.Data.Config;
using _Project.Code.Data.Enum;
using _Project.Code.Data.PersistentProgress;
using _Project.Code.Services.SaveLoad;
using _Project.Code.Services.Wallet;
using _Project.Code.UI.View.State.Lobby;
using UnityEngine;

namespace _Project.Code.Services.MenuShop
{
    public class MenuShopService
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly WalletService _walletService;
        private readonly ShopStateView _shopStateView;
        private readonly MenuShopConfig _menuShopConfig;

        public MenuShopService(ISaveLoadService saveLoadService
            , ConfigProvider configProvider
            , WalletService walletService
            , ShopStateView shopStateView)
        {
            _saveLoadService = saveLoadService;
            _walletService = walletService;
            _shopStateView = shopStateView;
            _menuShopConfig = configProvider.GetSingle<MenuShopConfig>();
        }

        public void Purchase(UpgradeType upgradeType, TowerType towerType)
        {
            var playerProgress = _saveLoadService.Load();
            var upgradeData = GetTowerUpgradeData(towerType);

            if (GetCurrentLevel(upgradeData, upgradeType) >= GetMaxLevel(towerType, upgradeType))
                return;

            int price = GetUpgradePrice(towerType, upgradeType);

            if (_walletService.MenuCoins.Value < price)
                return;

            switch (upgradeType)
            {
                case UpgradeType.Damage:
                    upgradeData.DamageLevel++;
                    break;
                case UpgradeType.Range:
                    upgradeData.RangeLevel++;
                    break;
                case UpgradeType.FireRate:
                    upgradeData.FireRateLevel++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null);
            }

            switch (towerType)
            {
                case TowerType.Assault:
                    playerProgress.UpgradeData.AssaultUpgradeData = upgradeData;
                    break;
                case TowerType.Demoman:
                    playerProgress.UpgradeData.DemoManUpgradeData = upgradeData;
                    break;
                case TowerType.Sniper:
                    playerProgress.UpgradeData.SniperUpgradeData = upgradeData;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(towerType), towerType, null);
            }

            _walletService.ReduceMenuCoins(price);
            playerProgress.WalletData.MenuCoins = _walletService.MenuCoins.Value;

            _saveLoadService.Save(playerProgress);
            _shopStateView.UpdateUI(GetTowerUpgradeData(towerType));
        }

        public TowerUpgradeData GetTowerUpgradeData(TowerType towerType)
        {
            var playerProgress = _saveLoadService.Load();

            return towerType switch
            {
                TowerType.Assault => playerProgress.UpgradeData.AssaultUpgradeData,
                TowerType.Demoman => playerProgress.UpgradeData.DemoManUpgradeData,
                TowerType.Sniper => playerProgress.UpgradeData.SniperUpgradeData,
                _ => throw new ArgumentOutOfRangeException(nameof(towerType), towerType, null)
            };
        }

        public int GetUpgradePrice(TowerType towerType, UpgradeType upgradeType)
        {
            var upgradeData = GetTowerUpgradeData(towerType);

            _menuShopConfig.Items.TryGetValue(towerType, out var towerConfig);

            int currentLevel = upgradeType switch
            {
                UpgradeType.Damage => upgradeData.DamageLevel,
                UpgradeType.Range => upgradeData.RangeLevel,
                UpgradeType.FireRate => upgradeData.FireRateLevel,
                _ => throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null)
            };

            int[] priceArray = upgradeType switch
            {
                UpgradeType.Damage => towerConfig.DamageUpgradePrices,
                UpgradeType.Range => towerConfig.RangeUpgradePrices,
                UpgradeType.FireRate => towerConfig.FireRateUpgradePrices,
                _ => throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null)
            };

            if (currentLevel >= priceArray.Length)
                return -1;

            return priceArray[currentLevel];
        }

        private int GetCurrentLevel(TowerUpgradeData upgradeData, UpgradeType upgradeType)
        {
            return upgradeType switch
            {
                UpgradeType.Damage => upgradeData.DamageLevel,
                UpgradeType.Range => upgradeData.RangeLevel,
                UpgradeType.FireRate => upgradeData.FireRateLevel,
                _ => throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null)
            };
        }

        private int GetMaxLevel(TowerType towerType, UpgradeType upgradeType)
        {
            var towerConfig = _menuShopConfig.Items[towerType];

            return upgradeType switch
            {
                UpgradeType.Damage => towerConfig.DamageUpgradePrices.Length,
                UpgradeType.Range => towerConfig.RangeUpgradePrices.Length,
                UpgradeType.FireRate => towerConfig.FireRateUpgradePrices.Length,
                _ => throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null)
            };
        }
    }
}