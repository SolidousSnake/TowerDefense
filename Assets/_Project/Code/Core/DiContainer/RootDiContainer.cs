using _Project.Code.Core.AssetManagement;
using _Project.Code.Core.SceneManagement;
using _Project.Code.Services.SaveLoad;
using _Project.Code.Services.Wallet;
using _Project.Code.Utils;
using VContainer;

namespace _Project.Code.Core.DiContainer
{
    public sealed class RootDiContainer : DiContainerBase
    {
        protected override void AddDependencies(IContainerBuilder builder)
        {
            BindServices(builder);
            builder.AddSingleton<ISceneLoader, SceneLoader>();
            builder.AddSingleton<IAssetProvider, ResourcesAssetProvider>();

            builder.AddSingleton<ConfigProvider>();
        }

        private void BindServices(IContainerBuilder builder)
        {
            builder.AddSingleton<WalletService>();
            builder.AddSingleton<ISaveLoadService, JsonSaveLoadService>();
        }
    }
}
