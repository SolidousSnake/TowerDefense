using System;
using _Project.Code.Core.AssetManagement;
using _Project.Code.Core.Factory;
using _Project.Code.Core.Fsm;
using _Project.Code.Data.Config;
using _Project.Code.Data.PersistentProgress;
using _Project.Code.Lobby.States;
using _Project.Code.Services.SaveLoad;
using _Project.Code.Services.Sound;
using _Project.Code.Services.Wallet;
using _Project.Code.Utils;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.Bootstrapper
{
    public class LobbyBootstrapper : IInitializable, IDisposable
    {
        [Inject] private readonly ConfigProvider _configProvider;
        [Inject] private readonly StateFactory _stateFactory;
        [Inject] private readonly LobbyStateMachine _fsm;
        [Inject] private readonly SoundService _soundService;
        [Inject] private readonly WalletService _walletService;
        [Inject] private readonly ISaveLoadService _saveLoadService;

        private readonly CompositeDisposable _cd = new CompositeDisposable();

        public void Initialize()
        {
            WarmUpAssets();

            var progress = _saveLoadService.Load();
            var soundData = progress.SoundData;
            _soundService.Initialize(soundData.MusicVolume, soundData.SfxVolume);

            _walletService.AddMenuCoins(progress.WalletData.MenuCoins);
            _soundService.SetMusicVolume(soundData.MusicVolume);
            _soundService.SetSfxVolume(soundData.SfxVolume);
            
            Subscribe(soundData, progress);


            CreateStates();

            _fsm.Enter<HubState>();
        }

        private void WarmUpAssets()
        {
            _configProvider.LoadSingle<MenuShopConfig>(AssetPath.Config.MenuShop);
            _configProvider.LoadSingle<ShopColors>(AssetPath.Config.MenuShopColors);
        }

        private void CreateStates()
        {
            _fsm.RegisterState(_stateFactory.Create<HubState>());
            _fsm.RegisterState(_stateFactory.Create<SettingState>());
            _fsm.RegisterState(_stateFactory.Create<SelectLevelState>());
            _fsm.RegisterState(_stateFactory.Create<ShopState>());
        }

        private void Subscribe(SoundData soundData, PlayerProgress progress)
        {
            _soundService.MusicVolume.Skip(1).Subscribe(volume =>
            {
                soundData.MusicVolume = volume;
                _saveLoadService.Save(progress);
            }).AddTo(_cd);

            _soundService.SfxVolume.Skip(1).Subscribe(volume =>
            {
                soundData.SfxVolume = volume;
                _saveLoadService.Save(progress);
            }).AddTo(_cd);
        }

        public void Dispose()
        {
            _configProvider.Release<ShopColors>();
            _cd.Dispose();
        }
    }
}