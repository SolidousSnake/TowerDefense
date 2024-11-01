using _Project.Code.Core.AssetManagement;
using _Project.Code.Core.DiContainer;
using _Project.Code.Core.Factory;
using _Project.Code.Core.SceneManagement;
using _Project.Code.Services.Input;
using _Project.Code.Services.SaveLoad;
using _Project.Code.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class RootDiContainer : DiContainerBase
{
    protected override void AddDependencies(IContainerBuilder builder)
    {
        BindServices(builder);
        builder.AddSingleton<ISceneLoader, SceneLoader>();
        builder.AddSingleton<IAssetProvider, ResourcesAssetProvider>();

        builder.AddSingleton<ConfigProvider>();
        builder.AddSingleton<StateFactory>();
    }

    private static void BindServices(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<InputService>();
        builder.AddSingleton<ISaveLoadService, JsonSaveLoadService>();

        // if(Application.isMobilePlatform)
            // builder.AddSingleton<IInputService, MobileInputService>();
        // else
            // builder.AddSingleton<IInputService, InputService>();
    }
}
