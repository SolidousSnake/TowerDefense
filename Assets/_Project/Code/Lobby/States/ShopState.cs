﻿using _Project.Code.Config;
using _Project.Code.Core.AssetManagement;
using _Project.Code.Core.Fsm;
using _Project.Code.UI.View.State.Lobby;
using _Project.Code.Utils;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Lobby.States
{
    public class ShopState : IState
    {
        private readonly ShopStateView _stateView;
        private readonly LobbyCameraRotation _config;
        private readonly Camera _camera;

        public ShopState(ConfigProvider configProvider,
            ShopStateView stateView)
        {
            _config = configProvider.GetSingleImmediately<LobbyCameraRotation>(AssetPath.Config.ShopCameraRotation);
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