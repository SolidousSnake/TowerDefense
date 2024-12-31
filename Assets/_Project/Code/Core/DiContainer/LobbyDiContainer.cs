using _Project.Code.Core.Bootstrapper;
using _Project.Code.Core.Fsm;
using _Project.Code.Services.MenuShop;
using _Project.Code.UI.View.State.Lobby;
using _Project.Code.Utils;
using NaughtyAttributes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.DiContainer
{
    public sealed class LobbyDiContainer : DiContainerBase
    {
        [BoxGroup("UI")] [SerializeField] private HubStateView _hubStateView;
        [BoxGroup("UI")] [SerializeField] private ShopStateView _shopStateView;
        [BoxGroup("UI")] [SerializeField] private SettingStateView _settingStateView;
        [BoxGroup("UI")] [SerializeField] private SelectLevelStateView _selectLevelStateView;

        protected override void AddDependencies(IContainerBuilder builder)
        {
            builder.RegisterInstance(_hubStateView);
            builder.RegisterInstance(_shopStateView);
            builder.RegisterInstance(_settingStateView);
            builder.RegisterInstance(_selectLevelStateView);
           
            builder.AddSingleton<MenuShopService>();
            builder.AddSingleton<LobbyStateMachine>();
            builder.RegisterEntryPoint<LobbyBootstrapper>();
        }
    }
}