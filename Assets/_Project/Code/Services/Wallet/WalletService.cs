using System;
using UniRx;

namespace _Project.Code.Services.Wallet
{
    public class WalletService
    {
        /// <summary>
        /// Coins used in gameplay scenes for in-level purchases, such as buying towers.
        /// </summary>
        public readonly ReactiveProperty<int> GameplayCoins = new();
        
        /// <summary>
        /// Coins used in the menu for tower upgrades, such as increasing fire rate or damage.
        /// </summary>
        public readonly ReactiveProperty<int> MenuCoins = new();

        public void AddGameplayCoins(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException($"Amount should be positive. Received: {amount}");
            GameplayCoins.Value += amount;
        }

        public void AddMenuCoins(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException($"Amount should be positive. Received: {amount}");
            MenuCoins.Value += amount;
        }

        public void ReduceGameplayCoins(int amount) => GameplayCoins.Value -= amount;
        public void ReduceMenuCoins(int amount) => MenuCoins.Value -= amount;
    }
}