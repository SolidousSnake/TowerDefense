using _Project.Code.Core.AssetManagement;
using _Project.Code.Core.Factory;
using _Project.Code.Core.SceneManagement;
using _Project.Code.Services.SaveLoad;
using _Project.Code.Services.Settings;
using _Project.Code.Services.Sound;
using _Project.Code.Services.Wallet;
using _Project.Code.UI;
using _Project.Code.Utils;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.DiContainer
{
    public sealed class RootDiContainer : DiContainerBase
    {
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private LoadingCurtain _loadingCurtain;
        
        protected override void AddDependencies(IContainerBuilder builder)
        {
            BindServices(builder);

            builder.RegisterInstance(_audioMixerGroup);
            builder.RegisterBuildCallback(container =>
                DontDestroyOnLoad(container.Instantiate(_loadingCurtain)));
            
            builder.AddSingleton<ISceneLoader, SceneLoader>();
            builder.AddSingleton<IAssetProvider, ResourcesAssetProvider>();
            
            builder.AddTransient<StateFactory>();
            builder.AddSingleton<ConfigProvider>();
        }

        private void BindServices(IContainerBuilder builder)
        {
            builder.AddSingleton<WalletService>();
            builder.AddSingleton<SettingService>();
            builder.AddSingleton<SoundService>();
            builder.AddSingleton<ISaveLoadService, JsonSaveLoadService>();
        }
    }
}
