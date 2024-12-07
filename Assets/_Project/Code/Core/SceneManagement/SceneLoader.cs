using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Code.Core.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public event Action OnLoadingStarted;
        public event Action OnLoadingCompleted;
        public event Action<float> OnLoadingProgress;

        public async UniTask Load(string nextScene)
        {
            OnLoadingStarted?.Invoke();

            var loadOperation = SceneManager.LoadSceneAsync(nextScene);
            loadOperation.allowSceneActivation = false;

            while (!loadOperation.isDone)
            {
                float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
                OnLoadingProgress?.Invoke(progress);
                if (loadOperation.progress >= 0.9f)
                    loadOperation.allowSceneActivation = true;

                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            OnLoadingCompleted?.Invoke();
        }
    }
}