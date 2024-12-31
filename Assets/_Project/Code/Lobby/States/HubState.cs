using _Project.Code.Core.AssetManagement;
using _Project.Code.Core.Fsm;
using _Project.Code.Data.Config;
using _Project.Code.UI;
using _Project.Code.Utils;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _Project.Code.Lobby.States
{
    public class HubState : IState
    {
        private readonly HubStateView _stateView;
        private readonly LobbyCameraRotation _config;
        private readonly Camera _camera;

        public HubState(ConfigProvider configProvider
            , HubStateView stateView)
        {
            _config = configProvider.GetSingleImmediately<LobbyCameraRotation>(AssetPath.Config.HubCameraRotation);
            _stateView = stateView;
            _camera = Camera.main;
        }
        
        public void Enter()
        {
            _stateView.Open();
            _camera.transform.DORotate(_config.Rotation, _config.Duration)
                .SetEase(_config.Ease).SetLink(_camera.gameObject);
        }

        public void Exit()
        {
            _stateView.Close();
        }
    }
}