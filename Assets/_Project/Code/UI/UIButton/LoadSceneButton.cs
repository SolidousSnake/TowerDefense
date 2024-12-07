using _Project.Code.Core.SceneManagement;
using NaughtyAttributes;
using UnityEngine;
using VContainer;
using UniRx;

namespace _Project.Code.UI.UIButton
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] [Scene] private string _scene;
        [SerializeField] private UnityEngine.UI.Button _button;
        [Inject] private ISceneLoader _sceneLoader;
       
        public void Start()
        {
            _button ??= GetComponent<UnityEngine.UI.Button>();
            _button.OnClickAsObservable().Subscribe(_ => _sceneLoader.Load(_scene)).AddTo(this);
        }
    }
}