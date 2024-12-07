using System;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Core.SceneManagement
{
    public interface ISceneLoader
    {
        public event Action OnLoadingStarted;
        public event Action OnLoadingCompleted;
        public event Action<float> OnLoadingProgress;
        public UniTask Load(string nextScene);
    }
}