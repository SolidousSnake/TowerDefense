using System;
using _Project.Code.Core.Factory;
using _Project.Code.Core.Fsm;
using _Project.Code.Data.PersistentProgress;
using _Project.Code.Lobby.States;
using _Project.Code.Services.SaveLoad;
using _Project.Code.Services.Sound;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.Bootstrapper
{
    public class LobbyBootstrapper : IInitializable, IDisposable
    {
        [Inject] private readonly StateFactory _stateFactory;
        [Inject] private readonly LobbyStateMachine _fsm;
        [Inject] private readonly SoundService _soundService;
        [Inject] private readonly ISaveLoadService _saveLoadService;

        private readonly CompositeDisposable _cd = new CompositeDisposable();

        public void Initialize()
        {
            var progress = _saveLoadService.Load();
            var soundData = progress.SoundData;
            _soundService.Initialize(soundData.MusicVolume, soundData.SfxVolume);

            Subscribe(soundData, progress);
            
            CreateStates();

            _fsm.Enter<HubState>();
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
            _cd.Dispose();
        }
    }
}