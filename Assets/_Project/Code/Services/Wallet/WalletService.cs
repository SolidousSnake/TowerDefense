using System;
using UniRx;

namespace _Project.Code.Services.Wallet
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WalletService
    {
        private readonly ReactiveProperty<int> _gameplayCoins;
        private readonly ReactiveProperty<int> _menuCoins;

        public WalletService()
        {
            _gameplayCoins = new ReactiveProperty<int>();
            GameplayCoins = _gameplayCoins.ToReadOnlyReactiveProperty();

            _menuCoins = new ReactiveProperty<int>();
            MenuCoins = _menuCoins.ToReadOnlyReactiveProperty();
        }

        /// <summary>
        /// Coins used in gameplay scenes for in-level purchases, such as buying towers.
        /// </summary>
        public IReadOnlyReactiveProperty<int> GameplayCoins { get; }
        
        /// <summary>
        /// Coins used in the menu for tower upgrades, such as increasing fire rate or damage.
        /// </summary>
        public IReadOnlyReactiveProperty<int> MenuCoins { get; }
        
        public void AddGameplayCoins(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException($"Amount should be positive. Received: {amount}");
            _gameplayCoins.Value += amount;
        }

        public void AddMenuCoins(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException($"Amount should be positive. Received: {amount}");
            _menuCoins.Value += amount;
        }

        public void ReduceGameplayCoins(int amount) => _gameplayCoins.Value -= amount;
        public void ReduceMenuCoins(int amount) => _menuCoins.Value -= amount;
    }
}