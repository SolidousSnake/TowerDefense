using Cysharp.Threading.Tasks;

namespace _Project.Code.Core.SceneManagement
{
    public interface ISceneLoader
    {
        public UniTask Load(string nextScene);
    }
}