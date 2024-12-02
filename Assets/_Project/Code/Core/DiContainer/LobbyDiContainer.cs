using _Project.Code.Core.Bootstrapper;
using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.DiContainer
{
    public sealed class LobbyDiContainer : DiContainerBase
    {
        
        protected override void AddDependencies(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LobbyBootstrapper>();
        }
    }
}