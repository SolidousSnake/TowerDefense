using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.Code.Core.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask Load(string nextScene)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
            await asyncOperation.ToUniTask();
        }
    }
}